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
        
        public CategoriesController()
        {
            _service = new CategoryService(_db);
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View(_service.GetActiveCategories());
        }
    }
}