using MediatR;

namespace Order.Book.Application.Queries;

public class GetOrderByIdQuery : IRequest<Domain.Entities.Order>
{
    public int OrderId { get; }

    public GetOrderByIdQuery(int orderId)
    {
        OrderId = orderId;
    }
}
