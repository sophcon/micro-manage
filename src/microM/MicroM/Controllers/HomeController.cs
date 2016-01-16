using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using MicroM.Hubs;

namespace MicroM.Controllers
{
    public class HomeController : Controller
    {
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