using MicroManage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MicroM.Services
{
    public class OrderService
    {
        public MicroManageContext Context { get; private set; }

        public OrderService (MicroManageContext context) {
            this.Context = context;
        }


        
    }
}