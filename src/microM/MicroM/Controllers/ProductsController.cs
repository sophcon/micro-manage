using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroM.Services;
using System.Threading.Tasks;
using MicroManage.Models;
namespace MicroM.Controllers
{
    public class ProductsController : _MicroController
    {

        private ProductService _service;

        [HttpGet]
        public PartialViewResult _Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> _Create(Product product)
        {
            await Task.Run(() => _service.CreateProduct(product));
            return Json(true);
        }

        [HttpGet]
        public PartialViewResult _List()
        {            
            return PartialView(_service.GetProducts());
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ProductsController()
        {
            _service = new ProductService(_db);
        }

    }
}