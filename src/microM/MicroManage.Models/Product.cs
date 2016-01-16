using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManage.Models
{
    public class Product : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool WebAvailable { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public int CategoryId { get; set; }
        public int[] AssemblyProductIds { get; set; }
    }
}
