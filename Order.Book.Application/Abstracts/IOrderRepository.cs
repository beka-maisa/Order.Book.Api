namespace Order.Book.Application.Abstracts;

public interface IOrderRepository
{
    Task<Domain.Entities.Order> AddOrderAsync(Domain.Entities.Order order);
    Task<Domain.Entities.Order> UpdateOrderAsync(Domain.Entities.Order order);
    Task<bool> DeleteOrderAsync(int orderId);
    Task<Domain.Entities.Order> GetOrderByIdAsync(int orderId);
    Task<List<Domain.Entities.Order>> GetAllOrdersAsync();
}