using Microsoft.EntityFrameworkCore;
using Order.Book.Application.Abstracts;
using Order.Book.Infrastructure.Context;

namespace Order.Book.Infrastructure.Concretes;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
       => _context = context;

    public async Task<Domain.Entities.Order> AddOrderAsync(Domain.Entities.Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<Domain.Entities.Order> UpdateOrderAsync(Domain.Entities.Order order)
    {
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null)
            return false;

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Domain.Entities.Order> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders.FindAsync(orderId);
    }

    public async Task<List<Domain.Entities.Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }
}