using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManage.Models
{
    public class Category : ModelBase
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
