using Cart.Models;
using System;
using System.Threading.Tasks;

namespace Cart.Contracts
{
    public interface IShoppingCartService
    {
        public Task<ShoppingCart> GetNewCart();
        public Task<ShoppingCart> GetCart(Guid id);
        public Task AddItem(ShoppingCart shoppingCart, ShoppingCartItem shoppingCartItem);
        public Task RemoveItem(ShoppingCart shoppingCart, ShoppingCartItem shoppingCartItem);
    }
}
