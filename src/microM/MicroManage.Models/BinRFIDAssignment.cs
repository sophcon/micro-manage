using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManage.Models
{
    public class BinRFIDAssignment : ModelBase
    {
        public string BinAddress { get; set; }
        [ForeignKey("Bin")]
        public int BinId { get; set; }

        public virtual Bin Bin { get; set; }
    }
}
