using MediatR;

namespace Order.Book.Application.Queries;

public class GetAllOrdersQuery : IRequest<List<Domain.Entities.Order>> { }
