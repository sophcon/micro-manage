using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroManage.Models;

namespace MicroM.Controllers
{
    public class PiController : Controller
    {
        [HttpPost]
        public ActionResult Interface(FormCollection frm)
        {
            return View();
        }


    }
} 