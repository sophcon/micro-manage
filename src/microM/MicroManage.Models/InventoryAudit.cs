using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManage.Models
{
    public class InventoryAudit : ModelBase
    {
        [ForeignKey("Inventory")]
        public int InventoryId { get; set; }
        public virtual ProductInventory Inventory { get; set; }
        public DateTime EventDate { get; set; }
        public InventoryStatus OldStatus { get; set; }
        public InventoryStatus NewStatus { get; set; }
        public string ModifiedBy { get; set; }
    }
}
