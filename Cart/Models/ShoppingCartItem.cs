using System;

namespace Cart.Models
{
    public class ShoppingCartItem
    {
        public Guid Id { get; set; }
        public string ExternalSource { get; set; }
        public string ExternalId { get; set; }
        public string Contents { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Total => UnitPrice * Quantity;
    }
}