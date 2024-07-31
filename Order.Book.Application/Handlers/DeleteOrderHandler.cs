using MediatR;
using Order.Book.Application.Commands;
using Order.Book.Application.Abstracts;
using Order.Book.Application.Notifications;

namespace Order.Book.Application.Handlers;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMediator _mediator;

    public DeleteOrderHandler(IOrderRepository orderRepository, IMediator mediator)
    {
        _orderRepository = orderRepository;
        _mediator = mediator;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);
        if (order == null)
            return false;

        await _orderRepository.DeleteOrderAsync(order.OrderId);

        // Publish the notification
        await _mediator.Publish(new OrderDeletedNotification { OrderId = request.OrderId });

        return true;
    }
}
