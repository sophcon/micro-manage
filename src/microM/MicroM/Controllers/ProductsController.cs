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

        [HttpPost]
        public async Task<JsonResult> _Create(FormCollection frm)
        {
            Product product = new Product();
            product.CategoryId = int.Parse(frm["category"]);
            product.Name = frm["name"];
            product.Price = decimal.Parse(frm["price"]);
            product.Count = 0;
            product.WebAvailable = false;
            product.Description = frm["description"];
            await Task.Run(() => ProductService.CreateProduct(product));
            return Json(true);
        }

        [HttpGet]
        public PartialViewResult _Edit(int id)
        {
            return PartialView(ProductService.GetProduct(id));
        }

        [HttpPost]
        public async Task<JsonResult> _Edit(Product product)
        {
            await Task.Run(() => ProductService.EditProduct(product));
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