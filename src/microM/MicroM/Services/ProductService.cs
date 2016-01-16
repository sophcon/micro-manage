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

        public async void CreateProduct(Product product)
        {
            Context.Products.Add(product);
            await Context.SaveChangesAsync();
        }

        public async void EditProduct(Product product)
        {
            Context.Products.Attach(product);
            Context.Entry(product).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public List<Product> GetProducts()
        {
            return Context.Products.OrderBy(p=>p.Name).ToList();
        }

        public Product GetProduct(int id)
        {
            return Context.Products.Find(id);
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