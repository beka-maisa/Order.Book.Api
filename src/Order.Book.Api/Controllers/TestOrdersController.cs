using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Order.Book.Api.OrderHub;

namespace Order.Book.Api.Controllers;

[ApiController]
[Route("api/")]
public class TestOrdersController : ControllerBase
{
    private readonly IHubContext<OrderBookHub> _hubContext;

    public TestOrdersController(IHubContext<OrderBookHub> hubContext)
       => _hubContext = hubContext;

    [HttpPost("send-update")]
    public async Task<IActionResult> SendUpdate(string orderUpdate)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveOrderUpdate", orderUpdate);
        return Ok();
    }

    [HttpPost("send-add-order-update")]
    public async Task<IActionResult> SendAddOrderUpdate(string orderUpdate)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveOrderAddUpdate", orderUpdate);
        return Ok();
    }

    [HttpPost("send-delete-order-update")]
    public async Task<IActionResult> SendDeleteOrderUpdate(string orderUpdate)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveOrderDeleteUpdate", orderUpdate);
        return Ok();
    }
}
