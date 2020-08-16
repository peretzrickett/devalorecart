using Cart.Contracts;
using Cart.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly CartContext _context;

        public ShoppingCartService(CartContext context)
        {
            _context = context;
        }

        public async Task AddItem(ShoppingCart shoppingCart, ShoppingCartItem newItem)
        {
            if (shoppingCart.Items.Any(i => i.ExternalId == newItem.ExternalId))
            {
                var item = shoppingCart.Items.Single(i => i.ExternalId == newItem.ExternalId);
                item.ExternalSource = newItem.ExternalSource;
                item.Contents = newItem.Contents;
                item.UnitPrice = newItem.UnitPrice;
                item.Quantity += newItem.Quantity;
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                shoppingCart.Items.Add(newItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ShoppingCart> GetCart(Guid id)
        {
            var cart = await _context.ShoppingCarts.Include(c => c.Items).SingleOrDefaultAsync(c => c.Id == id);
            return cart ?? await GetNewCart();
        }

        public async Task<ShoppingCart> GetNewCart()
        {
            var cart = new ShoppingCart { Id = new Guid(), CreatedOn = DateTime.Now, Items = new List<ShoppingCartItem>() };
            await _context.ShoppingCarts.AddAsync(cart);
            await _context.SaveChangesAsync();
            cart = await _context.ShoppingCarts.Include(c => c.Items).SingleOrDefaultAsync(c => c.Id == cart.Id);
            return cart;
        }

        public async Task RemoveItem(ShoppingCart shoppingCart, ShoppingCartItem shoppingCartItem)
        {
            var item = shoppingCart.Items.SingleOrDefault(i => i.Id == shoppingCartItem.Id || i.ExternalId == shoppingCartItem.ExternalId);
            if (item == null) return;

            item.Quantity -= shoppingCartItem.Quantity;
            _context.Entry(item).State = EntityState.Modified;

            if (item.Quantity < 1)
            {
                shoppingCart.Items.Remove(item);
                _context.Entry(shoppingCart).State = EntityState.Modified;

                if (shoppingCart.Items.Count == 0)
                {
                    _context.ShoppingCarts.Remove(shoppingCart);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
