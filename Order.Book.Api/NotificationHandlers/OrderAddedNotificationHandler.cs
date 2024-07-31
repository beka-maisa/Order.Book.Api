using MediatR;
using Microsoft.AspNetCore.SignalR;
using Order.Book.Api.OrderHub;
using Order.Book.Application.Notifications;

namespace Order.Book.Api.NotificationHandlers;

public class OrderAddedNotificationHandler : INotificationHandler<OrderAddedNotification>
{
    private readonly IHubContext<OrderBookHub> _hubContext;

    public OrderAddedNotificationHandler(IHubContext<OrderBookHub> hubContext)
      => _hubContext = hubContext;

    public async Task Handle(OrderAddedNotification notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.All.SendAsync("OrderAdded", notification.Order, cancellationToken);
    }
}
