using MicroM.Models;
using MicroManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroM.Services
{
    public class InventoryService
    {
        public MicroManageContext Context { get; private set; }
        public INotifierService NotifierService { get; private set; }

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
                Count = this.Context.ProductInventories.Where(m => m.ProductId == productId && m.Status == InventoryStatus.InStock).Count()
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
                this.Context.Entry(pi).State = System.Data.Entity.EntityState.Modified;
                await this.Context.SaveChangesAsync();
            }
            else {
                //insert record into productinventory if it doesn't exist
                ProductInventory newPI = new ProductInventory();
                newPI.ProductId = productId;
                newPI.SerialId = serialId;
                newPI.Status = InventoryStatus.InStock;

                //send to db
                this.Context.ProductInventories.Add(newPI);
                this.Context.SaveChanges();
            }

            //init change model
            InventorySummary change = new InventorySummary();
            change.ProductId = productId;

            //get count
            change.Count = this.Context.ProductInventories.Where(m => m.ProductId == productId && m.Status == InventoryStatus.InStock).Count();

            //set product count
            new ProductService(this.Context).UpdateProductCount(productId, change.Count);

            //send signalr notification of invetory change
            this.NotifierService.SendProductInventoryChange(change);
        }
    }
}