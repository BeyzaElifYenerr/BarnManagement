using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarnManagement.Models
{
    public class Cow : Animal
    {
        public static int StaticLifetimeDays = 3650;
        public Cow() { Species = Enums.Species.Cow; }
        public override int LifetimeDays => StaticLifetimeDays;

        public override Product ProduceProduct()
        {
            if (!IsAlive || Gender != Enums.Gender.Female) return null;
            return new Product
            {
                ProductType = Enums.ProductType.Milk,
                Quantity = 10,
                AnimalId = Id,
                ProducedAt = DateTime.UtcNow   // <<< EKLENDİ
            };
        }
    }
}
