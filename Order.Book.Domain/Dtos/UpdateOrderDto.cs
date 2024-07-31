using Order.Book.Domain.Enums;

namespace Order.Book.Domain.Dtos;

public class UpdateOrderDto
{
    public int UserId { get; set; }
    public OrderType OrderType { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
