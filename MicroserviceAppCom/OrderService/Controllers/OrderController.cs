using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersService _service;

        public OrderController(IOrdersService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _service.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(string id)
        {
            var order = await _service.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder( Order order)
        {
            var createdOrder = await _service.AddOrderAsync(order);
            if (createdOrder == null)
                return BadRequest("Invalid product or customer ID.");

            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(string id, Order order)
        {
            if (id != order.Id) return BadRequest();
            await _service.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(string id)
        {
            await _service.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}

