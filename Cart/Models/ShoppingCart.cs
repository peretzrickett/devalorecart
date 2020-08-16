using System;
using System.Collections.Generic;
using System.Linq;

namespace Cart.Models
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public int ItemCount => Items.Sum(i => i.Quantity);
        public double SumTotal => Items.Sum(i => i.Total);
    }
}
