using MediatR;
using Order.Book.Application.Abstracts;
using Order.Book.Application.Queries;

namespace Order.Book.Application.Handlers;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<Domain.Entities.Order>>
{
    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<Domain.Entities.Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllOrdersAsync();

        if (orders == null || orders.Count == 0)
            throw new KeyNotFoundException("No orders found.");

        return orders;
    }
}
