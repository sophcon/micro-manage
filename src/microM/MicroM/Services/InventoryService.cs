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

        private int GetProductCount(int productId) => this.Context.ProductInventories.Count(p => p.ProductId == productId && p.Status == InventoryStatus.InStock);

        public InventoryService(MicroManageContext context, INotifierService notifierService, IAuditService auditService, IProductService productService) {
            this.Context = context;
            this.NotifierService = notifierService;
            this.AuditService = auditService;
            this.ProductService = productService;
        }

        public async Task RemoveInventoryProduct(int productId, string serialId) {
            //find record with productid and change productinventory field to sold = 3
            var product = this.Context.ProductInventories.FirstOrDefault(m => m.SerialId == serialId);
            product.Status = InventoryStatus.Sold;

            //save changes
            this.Context.ProductInventories.Attach(product);
            this.Context.Entry(product).State = System.Data.Entity.EntityState.Modified;
            await this.Context.SaveChangesAsync();

            //init change model
            var change = new InventorySummary {
                ProductId = productId,
                Count = GetProductCount(productId)
            };

            //set product count
            new ProductService(this.Context).UpdateProductCount(productId, change.Count);

            //send signalr notification of invetory change
            this.NotifierService.SendProductInventoryChange(change);
        }

        public async Task<ProductInventory> GetInventoryBySerialIdAsync(string serialId) =>
            await Context.ProductInventories.FirstOrDefaultAsync(m => m.SerialId == serialId);

        public async Task UpdateInventory(int binId, string serialId) {
            var pi = await GetInventoryBySerialIdAsync(serialId);

            if (pi == null) {
                throw new ArgumentOutOfRangeException(nameof(serialId), $"The system cannot locate an inventory entry for serial id '{serialId}'.");
            }

            UpdateInventoryStatus(pi, InventoryStatus.InStock, binId);
        }

        public async Task AddSerializedItem (int Id, string serialId)
        {
            var inventoryEntry = await GetInventoryBySerialIdAsync(serialId);

            if (inventoryEntry != null) {
                throw new Exception("Serial Id is already in used");
            }

            this.Context.ProductInventories.Add(new ProductInventory {
                ProductId = Id,
                SerialId = serialId,
                Status = InventoryStatus.InStock
            });

            await this.Context.SaveChangesAsync();
            await PostSaveAsync(inventoryEntry, inventoryEntry.Status);
        }
        
        public async void UpdateInventoryStatus(ProductInventory inventoryEntry, InventoryStatus newStatus, int binId) {
            var oldStatus = inventoryEntry.Status;
            inventoryEntry.Status = newStatus;
            //inventoryEntry.BinId = binId;

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