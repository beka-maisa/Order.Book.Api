using Order.Book.Application.Commands;
using Order.Book.Application.Abstracts;
using MediatR;

namespace Order.Book.Application.Handlers;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderHandler(IOrderRepository orderRepository)
       => _orderRepository = orderRepository;

    public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the order from the repository
        var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);
        if (order == null)
            return false;

        order.UserId = request.UpdateOrderDto.UserId;
        order.OrderType = request.UpdateOrderDto.OrderType;
        order.Amount = request.UpdateOrderDto.Amount;
        order.Price = request.UpdateOrderDto.Price;
        order.OrderStatus = request.UpdateOrderDto.OrderStatus;
        order.UpdatedAt = DateTime.UtcNow;

        await _orderRepository.UpdateOrderAsync(order);

        return true;
    }
}
