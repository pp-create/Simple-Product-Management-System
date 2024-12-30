using Microsoft.EntityFrameworkCore;
using Simple_Product_Management_System.Data.Entity;
using System.Collections.Generic;

namespace Simple_Product_Management_System.Data.DBContext
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
