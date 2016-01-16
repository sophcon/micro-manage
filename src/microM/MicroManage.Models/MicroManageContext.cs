using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManage.Models {
    public class MicroManageContext : DbContext {
        public IDbSet<Address> Addresses { get; set; }
        public IDbSet<Bin> Bins { get; set; }
        public IDbSet<BinRFIDAssignment> BinRFIDAssignments { get; set; }
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<InventoryAudit> InventoryAudits { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<ProductInventory> ProductInventories { get; set; }
        public IDbSet<Category> Categories { get; set; }

        public MicroManageContext()
            : base("name=MicroManageContext") {

        }
    }
}
