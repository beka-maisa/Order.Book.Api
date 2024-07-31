using MediatR;
using Order.Book.Application.Commands;
using Order.Book.Application.Abstracts;
using Order.Book.Application.Notifications;

namespace Order.Book.Application.Handlers;

public class AddOrderHandler : IRequestHandler<AddOrderCommand, Domain.Entities.Order>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMediator _mediator;

    public AddOrderHandler(IOrderRepository orderRepository, IMediator mediator)
    {
        _orderRepository = orderRepository;
        _mediator = mediator;
    }

    public async Task<Domain.Entities.Order> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Domain.Entities.Order
        {
            OrderId = request.Order.OrderId,
            UserId = request.Order.UserId,
            Amount = request.Order.Amount,
            Price = request.Order.Price,
            OrderStatus = request.Order.OrderStatus,
            OrderType = request.Order.OrderType,
            CreatedAt = DateTime.UtcNow
        };

        await _orderRepository.AddOrderAsync(order);

        // Publish the notification
        await _mediator.Publish(new OrderAddedNotification { Order = order });

        return order;
    }
}
