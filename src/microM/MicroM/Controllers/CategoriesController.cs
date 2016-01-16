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
    public class CategoriesController : _MicroController
    {
        private CategoryService _service;

        [HttpGet]
        public PartialViewResult _Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> _Create(FormCollection frm)
        {
            if (frm["name"] != null)
            {
                await Task.Run(() => _service.AddCategory(frm["name"]));
                return Json(true);
            }
            else
            {
                return Json(false);
            }            
        }

        [HttpGet]
        public PartialViewResult _Edit(int id)
        {            
            return PartialView(_service.GetCategory(id));
        }

        [HttpPost]
        public async Task<JsonResult> _Edit(Category cat)
        {
            await Task.Run(() => _service.EditCategory(cat.Name, cat.Id));
            return Json(true);
        }

        [HttpGet]
        public PartialViewResult _List()
        {
            return PartialView(_service.GetCategories());
        }

        [HttpGet]
        public async  Task<JsonResult> Activate(int id)
        {
            await Task.Run(()=>_service.ActivateCategory(id));
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
        public async Task<JsonResult> Inactivate(int id)
        {
            await Task.Run(()=>_service.InactivateCategory(id));
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_service.GetActiveCategories());
        }

       
    }
}