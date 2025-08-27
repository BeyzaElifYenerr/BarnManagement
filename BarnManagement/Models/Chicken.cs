using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarnManagement.Models
{
    public class Chicken : Animal
    {
        public static int StaticLifetimeDays = 1825; // ~5 yıl
        public Chicken() { Species = Enums.Species.Chicken; }
        public override int LifetimeDays => StaticLifetimeDays;

        public override Product ProduceProduct()
        {
            if (!IsAlive || Gender != Enums.Gender.Female) return null;

            return new Product
            {
                ProductType = Enums.ProductType.Egg,
                Quantity = 1,
                AnimalId = Id,
                ProducedAt = DateTime.UtcNow
            };
        }
    }
}
