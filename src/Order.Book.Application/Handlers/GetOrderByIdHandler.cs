using MediatR;
using Order.Book.Application.Abstracts;
using Order.Book.Application.Queries;

namespace Order.Book.Application.Handlers;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Domain.Entities.Order>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdHandler(IOrderRepository orderRepository)
       => _orderRepository = orderRepository;

    public async Task<Domain.Entities.Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);
        if (order == null)
            throw new KeyNotFoundException($"Order with ID {request.OrderId} not found.");

        return order;
    }
}
