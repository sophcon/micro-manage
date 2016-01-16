using MicroManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroM.Services
{
    public class BinService
    {
        public MicroManageContext Context { get; private set; }
        public BinService(MicroManageContext context) {
            this.Context = context;
        }
        public IQueryable<Bin> GetBinsForSite(string site) {
            return this.Context.Bins.Where(b => b.Site.Equals(site));
        }
        public IQueryable<Bin> GetBins() {
            return this.Context.Bins;
        }
    }
}