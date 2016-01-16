using MicroManage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MicroM.Services
{
    public interface IProductService
    {
        void CreateProduct(Product product);
        void EditProduct(Product product);
        List<Product> GetProducts();
    }
    public class ProductService : IProductService
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
    }
}