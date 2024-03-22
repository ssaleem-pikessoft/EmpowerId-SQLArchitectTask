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
    public class Category
    {
        [Key]
        public Guid CategoryId {  get; set; }
        public required string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Product Product { get; set; }
    }
}
