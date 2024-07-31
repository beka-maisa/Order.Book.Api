using MediatR;
using Microsoft.AspNetCore.SignalR;
using Order.Book.Api.OrderHub;
using Order.Book.Application.Notifications;

namespace Order.Book.Api.NotificationHandlers;

public class OrderUpdatedNotificationHandler : INotificationHandler<OrderUpdatedNotification>
{
    private readonly IHubContext<OrderBookHub> _hubContext;

    public OrderUpdatedNotificationHandler(IHubContext<OrderBookHub> hubContext)
      => _hubContext = hubContext;

    public async Task Handle(OrderUpdatedNotification notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveOrderUpdate", notification.UpdatedOrder, cancellationToken);
    }
}
