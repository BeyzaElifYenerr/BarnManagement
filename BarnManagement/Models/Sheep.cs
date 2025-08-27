using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarnManagement.Models
{
    public class Sheep : Animal
    {
        public static int StaticLifetimeDays = 2920; // ~8 yıl
        public Sheep() { Species = Enums.Species.Sheep; }
        public override int LifetimeDays => StaticLifetimeDays;

        public override Product ProduceProduct()
        {
            if (!IsAlive || Gender != Enums.Gender.Female) return null;

            return new Product
            {
                ProductType = Enums.ProductType.Wool,
                Quantity = 2,
                AnimalId = Id,
                ProducedAt = DateTime.UtcNow
            };
        }
    }
}
