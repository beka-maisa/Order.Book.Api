using MediatR;

namespace Order.Book.Application.Commands;

public class AddOrderCommand : IRequest<Domain.Entities.Order>
{
    public Domain.Entities.Order Order { get; set; }

    public AddOrderCommand(Domain.Entities.Order order)
    {
        Order = order;
    }
}
