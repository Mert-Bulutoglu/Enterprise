using EnterpriseBusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _orderService.GetOrders();

            if (orders == null) {
                return NotFound();
            }

            return Ok(orders);
        }
    }
}
