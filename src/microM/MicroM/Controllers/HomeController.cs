using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using MicroM.Hubs;
using MicroM.Models;

namespace MicroM.Controllers
{
    public class HomeController : _MicroController
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

        public PartialViewResult List()
        {
            return PartialView();
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