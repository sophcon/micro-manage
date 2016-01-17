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
using System.Web.Http.Cors;

namespace MicroM.Controllers
{
    [EnableCors("*", "*", "*")]
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
        public async Task DispatchSerial(InventoryUpdateMessage message) {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MicroHub>();
            var notifierService = new NotifierService(hubContext);

            //notifierService
        }

        [HttpPost]
        public async Task<JsonResult> AdjustInventory(InventoryUpdateMessage message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MicroHub>();
            var notifierService = new NotifierService(hubContext);
            var auditService = new AuditService(_db);
            var productService = new ProductService(_db);
            var inventoryService = new InventoryService(_db, notifierService, auditService, productService);

            await inventoryService.UpdateInventory(message.ProductId, message.SerialId);

            return Json(true);
        }

        [HttpPost]
        public async Task<JsonResult> AddSerializedItem(InventoryUpdateMessage message) {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MicroHub>();
            var notifierService = new NotifierService(hubContext);
            var auditService = new AuditService(_db);
            var productService = new ProductService(_db);
            var inventoryService = new InventoryService(_db, notifierService, auditService, productService);

            await inventoryService.AddSerializedItem(message.ProductId, message.SerialId);

            return Json(true);
        }

        [HttpPost]
        public async Task<JsonResult> ReduceInventory(InventoryUpdateMessage message) {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MicroHub>();
            var notifierService = new NotifierService(hubContext);
            var auditService = new AuditService(_db);
            var productService = new ProductService(_db);
            var inventoryService = new InventoryService(_db, notifierService, auditService, productService);

            await inventoryService.AllocateInventoryItem(message.SerialId);

            return Json(true);
        }

        public ActionResult testView()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MicroHub>();
            var notifierService = new NotifierService(hubContext);
            var auditService = new AuditService(_db);
            var productService = new ProductService(_db);
            var inventoryService = new InventoryService(_db, notifierService, auditService, productService);

            

            inventoryService.AllocateInventoryItem(message.SerialId);

            return View();
        }

    }
} 