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

            if (action == "ADD" || action == "add")
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
            //try find matching productinventory record by using serialid
            ProductInventory pi = _db.ProductInventories.FirstOrDefault(m => m.SerialId == serialId);

            if(pi!=null)
            {
                //if the record already exists, change status to in stock = 1
                pi.Status = InventoryStatus.InStock;

                //send to db
                _db.ProductInventories.Attach(pi);
                _db.Entry(pi).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
            else
            {
                //insert record into productinventory if it doesn't exist
                ProductInventory newPI = new ProductInventory();
                newPI.ProductId = productId;
                newPI.SerialId = serialId;
                newPI.Status = InventoryStatus.InStock;

                //send to db
                _db.ProductInventories.Add(newPI);
                _db.SaveChanges();
            }

            //init change model
            ProductInventoryChangeViewModel change = new ProductInventoryChangeViewModel();
            change.ProductId = productId;

            //get count
            change.Count = _db.ProductInventories.Where(m => m.ProductId == productId && m.Status == InventoryStatus.InStock).Count();

            //set product count
            UpdateProductCount(productId, change.Count);

            //send signalr notification of invetory change
            SendProductInventoryChange(change);
        }

        private void RemoveInventroyProduct(int productId, string serialId)
        {
            //find record with productid and change productinventory field to sold = 3
            ProductInventory product = _db.ProductInventories.FirstOrDefault(m => m.SerialId == serialId);
            product.Status = InventoryStatus.Sold;

            //save changes
            _db.ProductInventories.Attach(product);
            _db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            //init change model
            ProductInventoryChangeViewModel change = new ProductInventoryChangeViewModel();
            change.ProductId = productId;

            //get count
            change.Count = _db.ProductInventories.Where(m => m.ProductId == productId && m.Status == InventoryStatus.InStock).Count();

            //set product count
            UpdateProductCount(productId, change.Count);

            //send signalr notification of invetory change
            SendProductInventoryChange(change);
        }

        private void SendProductInventoryChange(ProductInventoryChangeViewModel change)
        {
            //get context of hub
            var _context = GlobalHost.ConnectionManager.GetHubContext<MicroHub>();

            //send new data
            _context.Clients.All.productUpdate(change);

        }

        private void UpdateProductCount(int productId,int count)
        {
            //update product count
            Product product = _db.Products.FirstOrDefault(m => m.Id == productId);
            product.Count = count;

            //send to db
            _db.Products.Attach(product);
            _db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }

    }
} 