using Order.Book.Application.Abstracts;
using Microsoft.AspNetCore.SignalR;

namespace Order.Book.Api.OrderHub;

public class OrderBookHub : Hub
{
    private readonly IOrderRepository _orderRepository;

    public OrderBookHub(IOrderRepository orderRepository)
      => _orderRepository = orderRepository;

    public async Task AddOrder(Domain.Entities.Order order)
    {
        var addedOrder = await _orderRepository.AddOrderAsync(order);
        await Clients.All.SendAsync("OrderAdded", addedOrder);
    }

    public async Task UpdateOrder(Domain.Entities.Order order)
    {
        var updatedOrder = await _orderRepository.UpdateOrderAsync(order);
        await Clients.All.SendAsync("OrderUpdated", updatedOrder);
    }

    public async Task DeleteOrder(int orderId)
    {
        var result = await _orderRepository.DeleteOrderAsync(orderId);
        if (result)
        {
            await Clients.All.SendAsync("OrderDeleted", orderId);
        }
    }

    public async Task GetAllOrders()
    {
        var orders = await _orderRepository.GetAllOrdersAsync();
        await Clients.Caller.SendAsync("ReceiveAllOrders", orders);
    }

    public async Task GetOrderById(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        await Clients.Caller.SendAsync("ReceiveOrderById", order);
    }
}