using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace EmpowerId.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId {  get; set; }
        public required string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    }
}
