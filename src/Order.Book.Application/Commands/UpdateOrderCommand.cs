using MediatR;
using Order.Book.Domain.Dtos;

namespace Order.Book.Application.Commands;

public class UpdateOrderCommand : IRequest<bool>
{
    public int OrderId { get; }
    public UpdateOrderDto UpdateOrderDto { get; }

    public UpdateOrderCommand(int orderId, UpdateOrderDto updateOrderDto)
    {
        OrderId = orderId;
        UpdateOrderDto = updateOrderDto;
    }
}