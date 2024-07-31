using Order.Book.Domain.Enums;
using System.Text.Json.Serialization;

namespace Order.Book.Domain.Entities;

public class Order
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public OrderType OrderType { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
}
