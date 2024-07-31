using FluentValidation;

namespace Order.Book.Application.Validators;

public class OrderValidator : AbstractValidator<Domain.Entities.Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("Order ID must be greater than zero.");
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("User ID must be greater than zero.");
        RuleFor(x => x.OrderType).IsInEnum().WithMessage("Invalid order type.");
        RuleFor(x => x.OrderStatus).IsInEnum().WithMessage("Invalid order type.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}