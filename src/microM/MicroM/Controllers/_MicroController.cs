using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroManage.Models;

namespace MicroM.Controllers
{
    public class _MicroController : Controller
    {
        public MicroManageContext _db;

        public _MicroController()
        {
            _db = new MicroManageContext();
        }
    }
}