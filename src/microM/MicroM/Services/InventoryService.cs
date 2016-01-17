using MicroM.Models;
using MicroManage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MicroM.Services
{
    public class InventoryService
    {
        public MicroManageContext Context { get; private set; }
        public INotifierService NotifierService { get; private set; }
        public IAuditService AuditService { get; private set; }
        public IProductService ProductService { get; private set; }

        public int GetProductCount(int productId) => this.Context.ProductInventories.Count(p => p.ProductId == productId && p.Status == InventoryStatus.InStock);

        public InventoryService(MicroManageContext context, INotifierService notifierService, IAuditService auditService, IProductService productService) {
            this.Context = context;
            this.NotifierService = notifierService;
            this.AuditService = auditService;
            this.ProductService = productService;
        }

        public async Task AllocateInventoryItem(string serialId) {
            var inventoryEntry = await GetInventoryBySerialIdAsync(serialId);

            if (inventoryEntry == null) {
                throw new ArgumentOutOfRangeException(nameof(serialId), $"The system cannot locate an inventory entry for serial id '{serialId}'.");
            }

            await UpdateInventoryStatus(inventoryEntry, InventoryStatus.Allocated, inventoryEntry.BinId);
        }

        public async Task<ProductInventory> GetInventoryBySerialIdAsync(string serialId) =>
            await Context.ProductInventories.FirstOrDefaultAsync(m => m.SerialId == serialId);

        public async Task UpdateInventory(int binId, string serialId) {
            var pi = await GetInventoryBySerialIdAsync(serialId);

            if (pi == null) {
                throw new ArgumentOutOfRangeException(nameof(serialId), $"The system cannot locate an inventory entry for serial id '{serialId}'.");
            }

            await UpdateInventoryStatus(pi, InventoryStatus.InStock, binId);
        }

        public async Task AddSerializedItem (int productId, string serialId, int binId)
        {
            var inventoryEntry = await GetInventoryBySerialIdAsync(serialId);

            if (inventoryEntry != null) {
                throw new Exception("Serial Id is already in used");
            }
            inventoryEntry = new ProductInventory
            {
                ProductId = productId,
                SerialId = serialId,
                BinId = binId,
                Status = InventoryStatus.InStock
            };
            this.Context.ProductInventories.Add(inventoryEntry);

            await this.Context.SaveChangesAsync();
            await PostSaveAsync(inventoryEntry, inventoryEntry.Status);
        }
        
        public async Task UpdateInventoryStatus(ProductInventory inventoryEntry, InventoryStatus newStatus, int binId) {
            var oldStatus = inventoryEntry.Status;
            inventoryEntry.Status = newStatus;
            inventoryEntry.BinId = binId;

            this.Context.ProductInventories.Attach(inventoryEntry);
            this.Context.Entry(inventoryEntry).State = EntityState.Modified;
            await this.Context.SaveChangesAsync();
            await PostSaveAsync(inventoryEntry, oldStatus);
        }

        private async Task PostSaveAsync(ProductInventory inventoryEntry, InventoryStatus oldStatus) {
            await this.AuditService.AppendAuditEntryAsync(new InventoryAudit {
                EventDate = DateTime.Now,
                Inventory = inventoryEntry,
                InventoryId = inventoryEntry.Id,
                OldStatus = oldStatus,
                NewStatus = inventoryEntry.Status
            });

            this.NotifierService.SendProductInventoryChange(
                new InventorySummary {
                    ProductId = inventoryEntry.ProductId,
                    Count = GetProductCount(inventoryEntry.ProductId)
                });
        }
    }
}