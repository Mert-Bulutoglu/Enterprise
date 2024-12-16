using EnterpriseBusinessLayer.Abstract;
using EnterpriseEntityLayer.DTOs;
using EnterpriseEntityLayer.Models;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Text.Json;

namespace EnterpriseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IDatabase _redisDb;
        private readonly IConnectionMultiplexer _redis;

        public ProductController(IProductService productService, IConnectionMultiplexer redis)
        {
            _productService = productService;
            _redisDb = redis.GetDatabase();
            _redis = redis;
        }


        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost("purchase")]
        public IActionResult PurchaseProduct(PurchaseRequestDto purchaseRequestDto)
        {
            try
            {
                _productService.PurchaseProduct(purchaseRequestDto.ProductId, purchaseRequestDto.Quantity, purchaseRequestDto.UserId);
                return Ok(new { message = "Purchase successful." });

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("productDetail/{id}")]
        public IActionResult GetProductDetails(int id, string token)
        {

            var sessionData = _redisDb.StringGet($"session:{token}");
            if (string.IsNullOrEmpty(sessionData))
                return Unauthorized("Invalid or expired token.");

            var session = JsonSerializer.Deserialize<AuthSession>(sessionData);

            if (session.RoleName != "user" || !session.Permissions.Contains("blue"))
                return Forbid("Insufficient permissions.");

            var productDetail = _productService.GetProductDetail(id);
            if (productDetail == null)
                return NotFound("Product not found.");

            return Ok(productDetail);
        }


    }
}
