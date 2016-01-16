using MicroManage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MicroM.Services
{
    public class ProductService
    {
        public MicroManageContext Context { get; private set; }

        public ProductService (MicroManageContext context) {
            this.Context = context;
        }

        public List<Product> GetProducts()
        {
            return Context.Products.ToList();
        }

        public async void UpdateProductCount(int productId, int count) {
            //update product count
            Product product = Context.Products.Find(productId);
            product.Count = count;

            //send to db
            Context.Products.Attach(product);
            Context.Entry(product).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
    }
}