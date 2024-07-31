using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Book.Application.Commands;
using Order.Book.Application.Queries;
using Order.Book.Domain.Dtos;

namespace Order.Book.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}")]
[ApiController]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
       => _mediator = mediator;

    /// <summary>
    /// Adds a new order to the system.
    /// </summary>
    /// <param name="order">The order details to be added.</param>
    /// <returns>A newly created order with a status code indicating success and the location of the new resource.</returns>
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [HttpPost("create-order")]
    public async Task<IActionResult> AddOrder(Domain.Entities.Order order)
    {
        var createdOrder = await _mediator.Send(new AddOrderCommand(order));
        return Ok(createdOrder);
    }

    /// <summary>
    /// Retrieves an order by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order to retrieve.</param>
    /// <returns>The order details if found, otherwise a NotFound result.</returns>
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [HttpGet("get-order-by-id")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery(id));
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    /// <summary>
    /// Retrieves all orders in the system.
    /// </summary>
    /// <returns>A list of all orders.</returns>
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [HttpGet("get-all-orders")]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _mediator.Send(new GetAllOrdersQuery());
        return Ok(orders);
    }

    /// <summary>
    /// Updates the details of an existing order.
    /// </summary>
    /// <param name="id">The unique identifier of the order to update.</param>
    /// <param name="newOrderDetails">The new details of the order.</param>
    /// <returns>NoContent if the update is successful.</returns>
    [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [HttpPut("update-by-id")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDto updateOrderDto)
    {
        var result = await _mediator.Send(new UpdateOrderCommand(id, updateOrderDto));
        return result ? NoContent() : NotFound($"Order with ID {id} not found.");
    }

    /// <summary>
    /// Deletes an existing order by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order to delete.</param>
    /// <returns>NoContent if the deletion is successful, NotFound if the order is not found.</returns>
    [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [HttpDelete("delete-by-id")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var result = await _mediator.Send(new DeleteOrderCommand(id));
        if (!result)
            return NotFound();

        return NoContent();
    }
}