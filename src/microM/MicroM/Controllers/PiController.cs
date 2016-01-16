using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroManage.Models;
using MicroM.Models;
using Microsoft.AspNet.SignalR;
using MicroM.Hubs;

namespace MicroM.Controllers
{
    public class PiController : _MicroController
    {
        [HttpPost]
        public ActionResult Interface(FormCollection frm)
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegisterNewItem()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RegisterNewItem(FormCollection frm)
        {
            int productId = int.Parse(frm["productId"]);
            string serialId = frm["serialId"];
            string action = frm["action"];

            if (action == "ADD")
            {
                AddInventoryProduct(productId, serialId);
            }
            else
            {
                RemoveInventroyProduct(productId, serialId);
            }

            return Json(true);
        }

        private void AddInventoryProduct(int productId, string serialId)
        {
            //insert record into productinventory if it doesn't exist

            //if the record already exists, change status to in stock = 1


        }

        private void RemoveInventroyProduct(int productId, string serialId)
        {
            //find record with productid and change productinventory field to sold = 3
            ProductInventory product = _db.ProductInventories.FirstOrDefault(m => m.ProductId == productId);

            product.Status = InventoryStatus.Sold;

            //save changes
            _db.ProductInventories.Attach(product);
            _db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            //send signalr notification of invetory change
            
        }

        private void SendProductInventoryChange(ProductInventoryChangeViewModel change)
        {
            //get context of hub
            var _context = GlobalHost.ConnectionManager.GetHubContext<MicroHub>();

            //send new data
            _context.Clients.All.productUpdate(change);

        }

    }
} 