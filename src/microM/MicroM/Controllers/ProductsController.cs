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

        private ProductService ProductService;

        [HttpGet]
        public PartialViewResult _Create()
        {
            return PartialView();
        }

        public PartialViewResult _Edit(int id)
        {
            return PartialView(this.ProductService.GetProduct(id));
        }

        [HttpPost]
        public async Task<JsonResult> _Edit(Product cat)
        {
            await Task.Run(() => this.ProductService.EditProduct(cat));
            return Json(true);
        }

        [HttpPost]
        public async Task<JsonResult> _Create(Product product)
        {
            await Task.Run(() => this.ProductService.CreateProduct(product));
            return Json(true);
        }

        [HttpGet]
        public PartialViewResult _List()
        {            
            return PartialView(this.ProductService.GetProducts());
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ProductsController()
        {
            this.ProductService = new ProductService(_db);
        }

    }
}