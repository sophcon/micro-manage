using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using MicroM.Hubs;
using MicroM.Models;
using MicroManage.Models;
using MicroM.Services;

namespace MicroM.Controllers
{
    public class HomeController : _MicroController
    {
        public object ProductService { get; private set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public PartialViewResult List()
        {

            
            return PartialView();
        }

        [HttpGet]
        public JsonResult Product()
        {

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MicroHub>();
            var notifierService = new NotifierService(hubContext);
            var auditService = new AuditService(_db);
            var productService = new ProductService(_db);
            var inventoryService = new InventoryService(_db, notifierService, auditService, productService);

            var products = this.ProductService;

            //products.Select(p => new
            //{
            //    Name = p.Name,
            //    Count = inventoryService.GetProductCount(p.Id),
            //    CategoryId = p.CategoryId,
            //    Description = p.Description,
            //    Price = p.Price,
            //    WebAvailable = p.WebAvailable,


            //}
                );

            return Json(products, JsonRequestBehavior .AllowGet);

        }

        [HttpGet]
        public ActionResult SignalRTest()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SignalRTest(FormCollection frm)
        {
            string msg = frm["message"];
            var _hubContext = GlobalHost.ConnectionManager.GetHubContext<MicroHub>();

            //send data to clients
            _hubContext.Clients.All.testEvent(msg);

            return Json(msg); 
        }
    }
}