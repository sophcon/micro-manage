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
    public class BinsController : _MicroController
    {
        private BinService BinService;
        
        public BinsController()
        {
            BinService = new BinService(_db);
        }

        [HttpGet]
        public PartialViewResult _Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> _Create(Bin bin)
        {
            await BinService.CreateBin(bin);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult _Edit(int id)
        {
            return PartialView(BinService.GetBin(id));
        }

        [HttpPost]
        public ActionResult _Edit(Bin bin)
        {
            BinService.EditBin(bin);
            return RedirectToAction("Index");
        }  
        
        [HttpGet]
        public ActionResult Index()
        {
            return View(BinService.GetBins());
        }
    }
}