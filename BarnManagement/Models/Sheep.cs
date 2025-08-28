using System;

namespace BarnManagement.Models
{
    public class Sheep : Animal
    {
        public static int StaticLifetimeDays = 8;   
        public static int MaturityYears = 1;        

        public Sheep() { Species = Enums.Species.Sheep; }
        public override int LifetimeDays => StaticLifetimeDays;

        public override Product ProduceProduct()
        {
            if (!IsAlive || Gender != Enums.Gender.Female) return null;

            
            if (AgeDays < MaturityYears) return null;

            return new Product
            {
                ProductType = Enums.ProductType.Wool,
                Quantity = 4,                   
                AnimalId = Id,
                ProducedAt = DateTime.UtcNow
            };
        }
    }
}
