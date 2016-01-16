using MicroM.Models;
using MicroManage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MicroM.Services
{
    public class InventoryService
    {
        public MicroManageContext Context { get; private set; }
        public INotifierService NotifierService { get; private set; }
        //public IAuditService AuditService { get; private set; }

        private int GetProductCount(int productId) => this.Context.ProductInventories.Count(p => p.ProductId == productId && p.Status == InventoryStatus.InStock);

        public InventoryService(MicroManageContext context, INotifierService notifierService) {
            this.Context = context;
            this.NotifierService = notifierService;
        }

        public async void RemoveInventoryProduct(int productId, string serialId) {
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

        public ProductInventory GetInventoryBySerialId(string serialId) =>
            Context.ProductInventories.FirstOrDefault(m => m.SerialId == serialId);

        public void AddInventoryProduct(int binId, string serialId) {
            var pi = GetInventoryBySerialId(serialId);

            if (pi == null) {
                throw new ArgumentOutOfRangeException(nameof(serialId), $"The system cannot locate an inventory entry for serial id '{serialId}'.");
            }

            UpdateInventoryStatus(pi, InventoryStatus.InStock, binId);
        }

        public async void UpdateInventoryStatus(ProductInventory inventoryEntry, InventoryStatus newStatus, int binId) {
            var oldStatus = inventoryEntry.Status;
            inventoryEntry.Status = newStatus;
            //inventoryEntry.BinId = binId;

            this.Context.ProductInventories.Attach(inventoryEntry);
            this.Context.Entry(inventoryEntry).State = EntityState.Modified;
            await this.Context.SaveChangesAsync();

            new ProductService(this.Context).UpdateProductCount(inventoryEntry.ProductId, GetProductCount(inventoryEntry.ProductId));

            this.NotifierService.SendProductInventoryChange(
                new InventorySummary {
                    ProductId = inventoryEntry.ProductId,
                    Count = GetProductCount(inventoryEntry.ProductId)
                });
        }
    }
}