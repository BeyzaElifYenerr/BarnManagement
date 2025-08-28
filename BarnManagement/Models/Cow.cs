using System;

namespace BarnManagement.Models
{
    public class Cow : Animal
    {
        public static int StaticLifetimeDays = 11;   
        public static int MaturityYears = 2;         

        public Cow() { Species = Enums.Species.Cow; }
        public override int LifetimeDays => StaticLifetimeDays;

        public override Product ProduceProduct()
        {
            if (!IsAlive || Gender != Enums.Gender.Female) return null;

            
            if (AgeDays < MaturityYears) return null;

            return new Product
            {
                ProductType = Enums.ProductType.Milk,
                Quantity = 2400,              
                AnimalId = Id,
                ProducedAt = DateTime.UtcNow
            };
        }
    }
}
