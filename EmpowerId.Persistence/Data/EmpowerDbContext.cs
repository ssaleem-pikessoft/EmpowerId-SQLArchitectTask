using EmpowerId.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpowerId.Persistence.Data
{
    public class EmpowerDbContext: DbContext
    {
        public EmpowerDbContext(DbContextOptions<EmpowerDbContext> options): base(options) 
        {

        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmpowerDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
