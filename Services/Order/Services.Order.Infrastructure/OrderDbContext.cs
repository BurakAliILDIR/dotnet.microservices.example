using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Services.Order.Domain.Aggregate;

namespace Services.Order.Infrastructure
{
    public class OrderDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "ordering";

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.Aggregate.Order> Orders { get; set; }
        public DbSet<Domain.Aggregate.OrderItem> OrderItems { get; set; }

        public override int SaveChanges()
        {
            // event

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Aggregate.Order>().ToTable("Orders", DEFAULT_SCHEMA);
            modelBuilder.Entity<Domain.Aggregate.OrderItem>().ToTable("OrderItems", DEFAULT_SCHEMA);

            modelBuilder.Entity<OrderItem>().Property(x=> x.Price).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Domain.Aggregate.Order>().OwnsOne(x=>x.Address).WithOwner();

            base.OnModelCreating(modelBuilder);
        }
    }
}