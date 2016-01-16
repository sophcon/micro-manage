using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroManage.Models;
using MicroM.Models;
using Microsoft.AspNet.SignalR;
using MicroM.Hubs;
using MicroM.Services;
using System.Threading.Tasks;

namespace MicroM.Controllers
{
    public class PiController : _MicroController
    {
        [HttpPost]
        public ActionResult Interface(FormCollection frm)
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddInventory()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AddInventory(InventoryUpdateMessage message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MicroHub>();
            var notifierService = new NotifierService(hubContext);
            var inventoryService = new InventoryService(_db, notifierService);

            await Task.Run(() => inventoryService.AddInventoryProduct(message.ProductId, message.SerialId));

            return Json(true);
        }

        public async Task<JsonResult> RemoveInventory(InventoryUpdateMessage message) {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MicroHub>();
            var notifierService = new NotifierService(hubContext);
            var inventoryService = new InventoryService(_db, notifierService);

            await Task.Run(() => inventoryService.RemoveInventoryProduct(message.ProductId, message.SerialId));

            return Json(true);
        }
    }
} 