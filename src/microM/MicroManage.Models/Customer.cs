using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManage.Models
{
    public class Customer : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int MailToAddressId { get; set; }
        public Address MailToAddress { get; set; }

        public int ShipToAddressId { get; set; }
        public Address ShipAddress { get; set; }
    }
}
