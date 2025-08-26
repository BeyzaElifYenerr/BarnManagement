using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarnManagement.Models
{
    public class Product
    {
        public int Id { get; set; }
        public ProductType ProductType { get; set; }
        public int Quantity { get; set; }
        public DateTime ProducedAt { get; set; }
        public bool IsSold { get; set; } = false;

        public int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }
    }
}
