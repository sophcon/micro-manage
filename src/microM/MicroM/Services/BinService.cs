using MicroManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MicroM.Services
{
    public class BinService
    {
        public MicroManageContext Context { get; private set; }
        public BinService(MicroManageContext context) {
            this.Context = context;
        }

        public async Task CreateBin(Bin bin)
        {
            Context.Bins.Add(bin);
            await Context.SaveChangesAsync();
        }

        public async Task EditBin(Bin bin)
        {
            Context.Bins.Attach(bin);
            Context.Entry(bin).State = System.Data.Entity.EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public Bin GetBin(int id)
        {
            return Context.Bins.Find(id);
        }

        public IQueryable<Bin> GetBinsForSite(string site) {
            return this.Context.Bins.Where(b => b.Site.Equals(site));
        }
        public List<Bin> GetBins() {
            return this.Context.Bins.ToList();
        }
    }
}