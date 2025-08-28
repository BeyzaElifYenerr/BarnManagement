using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarnManagement.Models
{
    public class Product
    {
        public int Id { get; set; }
        public Enums.ProductType ProductType { get; set; }
        public int Quantity { get; set; }
        public bool IsSold { get; set; } = false;
        public int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }

        public int BarnId { get; set; }
        public virtual Barn Barn { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ProducedAt { get; set; } = DateTime.UtcNow;
    }
}
