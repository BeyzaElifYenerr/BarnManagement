using System;

namespace BarnManagement.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total => UnitPrice * Quantity;
        public DateTime SoldAt { get; set; } = DateTime.UtcNow;
    }
}
