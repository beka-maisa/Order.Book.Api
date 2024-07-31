using MediatR;
using Microsoft.AspNetCore.SignalR;
using Order.Book.Api.OrderHub;
using Order.Book.Application.Notifications;

namespace Order.Book.Api.NotificationHandlers;

public class OrderDeletedNotificationHandler : INotificationHandler<OrderDeletedNotification>
{
    private readonly IHubContext<OrderBookHub> _hubContext;

    public OrderDeletedNotificationHandler(IHubContext<OrderBookHub> hubContext)
      => _hubContext = hubContext;

    public async Task Handle(OrderDeletedNotification notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.All.SendAsync("OrderDeleted", notification, cancellationToken);
    }
}