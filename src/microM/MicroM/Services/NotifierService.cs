using MicroM.Hubs;
using MicroM.Models;
using MicroManage.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroM.Services
{
    public interface INotifierService
    {
        void SendProductInventoryChange(InventorySummary change);
    }
    public class NotifierService : INotifierService
    {
        public IHubContext HubContext { get; private set; }

        public NotifierService(IHubContext context) {
            this.HubContext = context;
        }

        public void SendProductInventoryChange(InventorySummary change) {
            this.HubContext.Clients.All.productUpdate(change);
        }
    }
}