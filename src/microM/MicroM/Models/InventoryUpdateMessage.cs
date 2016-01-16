using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace MicroM.Models
{
    public class InventoryUpdateMessage
    {
        public int ProductId { get; set; }
        public string SerialId { get; set; }
    }
}