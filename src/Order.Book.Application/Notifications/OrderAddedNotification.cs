using MediatR;

namespace Order.Book.Application.Notifications;

public class OrderAddedNotification : INotification
{
    public Domain.Entities.Order Order { get; set; }
}
