using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroM.Services;
using MicroManage.Models;

namespace MicroM.Controllers
{
    public class CategoriesController : _MicroController
    {
        private CategoryService _service;

        [HttpGet]
        public JsonResult Activate(int id)
        {
            _service.ActivateCategory(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        
        public CategoriesController()
        {
            _service = new CategoryService(_db);
        }

        [HttpPost]
        public JsonResult Edit(FormCollection frm)
        {
            int id = int.Parse(frm["id"]);
            string name = frm["name"];

            _service.EditCategory(name, id);

            return Json(true);
        }

        [HttpGet]
        public JsonResult Inactivate(int id)
        {
            _service.InactivateCategory(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_service.GetActiveCategories());
        }

       
    }
}