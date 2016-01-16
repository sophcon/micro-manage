using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManage.Models
{
    public class Bin : ModelBase {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Site { get; set; }
        public int AssignedProductId {get;set;}
    }
}
