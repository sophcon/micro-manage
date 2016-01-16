using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroM.Services;

namespace MicroM.Controllers
{
    public class ProductsController : _MicroController
    {

        public ProductsController()
        {
            _service = new ProductService(_db);
        }

        private ProductService _service;
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult _List()
        {
            
            return PartialView();
        }
    }
}