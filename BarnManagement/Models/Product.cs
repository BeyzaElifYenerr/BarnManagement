using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Column(TypeName = "datetime2")]
        public DateTime ProducedAt { get; set; } = DateTime.UtcNow;
    }
}
