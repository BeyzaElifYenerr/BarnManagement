using System;

namespace BarnManagement.Models
{
    public class Chicken : Animal
    {
        public static int StaticLifetimeDays = 4;   
        public static int MaturityYears = 0;        

        public Chicken() { Species = Enums.Species.Chicken; }
        public override int LifetimeDays => StaticLifetimeDays;

        public override Product ProduceProduct()
        {
            if (!IsAlive || Gender != Enums.Gender.Female) return null;

           
            if (AgeDays < MaturityYears) return null;

            return new Product
            {
                ProductType = Enums.ProductType.Egg,
                Quantity = 290,                
                AnimalId = Id,
                ProducedAt = DateTime.UtcNow
            };
        }
    }
}
