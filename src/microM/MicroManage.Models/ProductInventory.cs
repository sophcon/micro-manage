using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManage.Models
{
    public class ProductInventory : ModelBase
    {
        public int ProductId { get; set; }
        public string SerialId { get; set; }
        public int BinId { get; set; }
        public InventoryStatus Status { get; set; }
    }

    public enum InventoryStatus : int {
        InStock = 1,
        Allocated = 2,
        Sold = 3
    }
}
