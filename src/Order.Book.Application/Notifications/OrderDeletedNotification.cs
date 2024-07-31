using MediatR;

namespace Order.Book.Application.Notifications;

public class OrderDeletedNotification : INotification
{
    public int OrderId { get; set; }
}
