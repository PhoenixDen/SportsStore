using System;
using System.Data.Entity;

namespace SportsStore.Models.Repository
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}