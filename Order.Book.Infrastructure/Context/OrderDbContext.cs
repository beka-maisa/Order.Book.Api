using Microsoft.EntityFrameworkCore;

namespace Order.Book.Infrastructure.Context;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public DbSet<Domain.Entities.Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Order>()
            .Property(o => o.OrderId)
            .UseIdentityColumn();

        base.OnModelCreating(modelBuilder);
    }
}