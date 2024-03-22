using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace EmpowerId.Entities
{
    public class Product
    {
        [Key]
        public Guid ProductId {  get; set; }
        public Guid CategoryId { get; set; }
        public required string ProductName {  get; set; }
        public string? Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public Category Category { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
