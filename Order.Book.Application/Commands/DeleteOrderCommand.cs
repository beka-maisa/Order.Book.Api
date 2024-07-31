using MediatR;

namespace Order.Book.Application.Commands;

public class DeleteOrderCommand : IRequest<bool>
{
    public int OrderId { get; }

    public DeleteOrderCommand(int orderId)
    {
        OrderId = orderId;
    }
}
