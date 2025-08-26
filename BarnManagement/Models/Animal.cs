using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarnManagement.Models
{
    public abstract class Animal
    {
        public int Id { get; set; }
        public Species Species { get; set; }
        public int AgeDays { get; set; }
        public Gender Gender { get; set; }
        public bool IsAlive { get; set; } = true;

       
        public static int DefaultLifetimeDays = 3650; 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
        public abstract Product ProduceProduct();
    }

    public class Cow : Animal
    {
        public static int LifetimeDays = 3650; 
        public Cow() { Species = Species.Cow; }

        public override Product ProduceProduct()
        {
            
            return new Product { ProductType = ProductType.Milk, Quantity = 10, ProducedAt = DateTime.UtcNow, AnimalId = Id };
        }
    }

    public class Chicken : Animal
    {
        public static int LifetimeDays = 1825; 
        public Chicken() { Species = Species.Chicken; }

        public override Product ProduceProduct()
        {
            return new Product { ProductType = ProductType.Egg, Quantity = 1, ProducedAt = DateTime.UtcNow, AnimalId = Id };
        }
    }

    public class Sheep : Animal
    {
        public static int LifetimeDays = 2920; 
        public Sheep() { Species = Species.Sheep; }

        public override Product ProduceProduct()
        {
            return new Product { ProductType = ProductType.Wool, Quantity = 2, ProducedAt = DateTime.UtcNow, AnimalId = Id };
        }
    }

}
