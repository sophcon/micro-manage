using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManage.Models
{
    public class Order : ModelBase
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }

}
