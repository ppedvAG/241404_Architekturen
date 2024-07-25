using Microsoft.EntityFrameworkCore;
using ppedv.PizzaOrderManager.Model;
using System.Diagnostics;

namespace ppedv.PizzaOrderManager.Data.EfCore
{
    public class EfContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Topping> Toppings { get; set; }

        string conString;

        public EfContext(string conString)
        {
            this.conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conString)
                          .LogTo(msg => Debug.WriteLine(msg));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasMany(x => x.AsBilling).WithOne(x => x.BillingAddress);
            modelBuilder.Entity<Address>().HasMany(x => x.AsDeliver).WithOne(x => x.DeliveryAddress);
        }

    }
}
