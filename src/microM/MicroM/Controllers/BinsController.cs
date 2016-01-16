using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroM.Services;
using MicroManage.Models;
using System.Threading.Tasks;

namespace MicroM.Controllers
{
    public class BinsController : _MicroController
    {
        private CategoryService _service;

        public ActionResult Index()
        {
            return View();
        }
    }
}