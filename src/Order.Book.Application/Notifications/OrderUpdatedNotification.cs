using MediatR;

namespace Order.Book.Application.Notifications;

public class OrderUpdatedNotification : INotification
{
    public Domain.Entities.Order UpdatedOrder { get; }

    public OrderUpdatedNotification(Domain.Entities.Order updatedOrder)
    {
        UpdatedOrder = updatedOrder;
    }
}
