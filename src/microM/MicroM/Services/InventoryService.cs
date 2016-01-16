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

        public async void AddInventoryProduct(int productId, string serialId) {
            //try find matching productinventory record by using serialid
            var pi = this.Context.ProductInventories.FirstOrDefault(m => m.SerialId == serialId);

            if (pi != null) {
                //if the record already exists, change status to in stock = 1
                pi.Status = InventoryStatus.InStock;

                //send to db
                this.Context.ProductInventories.Attach(pi);
                this.Context.Entry(pi).State = EntityState.Modified;
                await this.Context.SaveChangesAsync();
            }
            else {
                //send to db
                this.Context.ProductInventories.Add(
                    new ProductInventory {
                        ProductId = productId,
                        SerialId = serialId,
                        Status = InventoryStatus.InStock
                    });

                await this.Context.SaveChangesAsync();
            }

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
    }
}