using Microsoft.EntityFrameworkCore;

namespace Cart.Models
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options)
        {
        }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> Items { get; set; }
    }
}
