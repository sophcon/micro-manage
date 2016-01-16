using MicroManage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MicroM.Services
{
    public class AddressService
    {
        public MicroManageContext Context { get; private set; }

        public AddressService (MicroManageContext context) {
            this.Context = context;
        }

        public async void CreateAddress(Address address)
        {
            Context.Addresses.Add(address);
            await Context.SaveChangesAsync();
        }

        public async void EditAddress(Address address)
        {
            Context.Addresses.Attach(address);
            Context.Entry(address).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public Address GetAddress(int id)
        {
            Address address = Context.Addresses.Find(id);
            return address;
        }
        
    }
}