using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace WebApiSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders() => Ok(await _orderService.GetAllOrdersAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound("Order not found.");
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(Order order)
        {
            await _orderService.AddOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.Id) return BadRequest("ID mismatch.");
            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomer(int customerId)
            => Ok(await _orderService.GetOrdersByCustomerIdAsync(customerId));

        [HttpGet("customer/{customerId}/total")]
        public async Task<IActionResult> GetTotalPriceForCustomer(int customerId)
            => Ok(await _orderService.GetTotalPriceForCustomerAsync(customerId));

        [HttpGet("totals")]
        public async Task<IActionResult> GetTotalPricePerCustomer()
            => Ok(await _orderService.GetTotalPricePerCustomerAsync());
    }
}