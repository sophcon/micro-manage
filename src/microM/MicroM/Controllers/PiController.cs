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
        public ActionResult RegisterNewItem()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RegisterNewItem(FormCollection frm)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MicroHub>();
            var notifierService = new NotifierService(hubContext);
            var inventoryService = new InventoryService(_db, notifierService);

            int productId = int.Parse(frm["productId"]);
            string serialId = frm["serialId"];
            string action = frm["action"];

            if (action == "ADD" || action == "add") {
                await Task.Run(() => inventoryService.AddInventoryProduct(productId, serialId));
            } else {
                await Task.Run(() => inventoryService.RemoveInventoryProduct(productId, serialId));
            }

            return Json(true);
        }
    }
} 