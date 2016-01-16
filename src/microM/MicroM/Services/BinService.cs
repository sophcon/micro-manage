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
    }
}