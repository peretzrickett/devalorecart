using Cart.Contracts;
using Cart.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _service;

        public ShoppingCartController(IShoppingCartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> Get()
        {
            return await _service.GetNewCart();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ShoppingCart>> Get(Guid id)
        {
            return await _service.GetCart(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ShoppingCart>> AddItem(Guid id, ShoppingCartItem item)
        {
            var cart = await _service.GetCart(id);
            await _service.AddItem(cart, item);
            return await _service.GetCart(id);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ShoppingCart>> RemoveItem(Guid id, ShoppingCartItem item)
        {
            var cart = await _service.GetCart(id);
            await _service.RemoveItem(cart, item);
            return await _service.GetCart(id);
        }
    }
}
