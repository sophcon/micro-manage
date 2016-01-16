using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManage.Models
{
    public class Customer : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ForeignKey("MailToAddress")]
        public int MailToAddressId { get; set; }
        public virtual Address MailToAddress { get; set; }

        [ForeignKey("ShipAddress")]
        public int ShipToAddressId { get; set; }
        public virtual Address ShipAddress { get; set; }
    }
}
