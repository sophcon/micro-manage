using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManage.Models
{
    public class BinRFIDAssignment : ModelBase
    {
        public string BinAddress { get; set; }
        public int BinId { get; set; }
        public Bin Bin { get; set; }
    }
}
