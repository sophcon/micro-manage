using MicroManage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MicroM.Services
{
    public class CategoryService
    {
        public MicroManageContext Context { get; private set; }

        public CategoryService (MicroManageContext context) {
            this.Context = context;
        }

        public async void AddCategory(string Name)
        {
            Category cat = new Category();
            cat.Name = Name;
            cat.IsActive = true;

            //send to db
            Context.Categories.Add(cat);
            await Context.SaveChangesAsync();
        }

        public async void ActivateCategory(int CategoryId)
        {
            Category cat = Context.Categories.Find(CategoryId);
            cat.IsActive = true;

            //send to db
            Context.Categories.Attach(cat);
            Context.Entry(cat).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async void EditCategory(string Name, int CategoryId)
        {
            Category cat = Context.Categories.Find(CategoryId);
            cat.Name = Name;

            //send to db
            Context.Categories.Attach(cat);
            Context.Entry(cat).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async void InactivateCategory(int CategoryId)
        {
            Category cat = Context.Categories.Find(CategoryId);
            cat.IsActive = false;

            //send to db
            Context.Categories.Attach(cat);
            Context.Entry(cat).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
    }
}