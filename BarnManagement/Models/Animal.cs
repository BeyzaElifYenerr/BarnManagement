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
        public Enums.Species Species { get; protected set; }
        public Enums.Gender Gender { get; set; }
        public int AgeDays { get; set; }
        public bool IsAlive { get; set; } = true;
        public abstract int LifetimeDays { get; }


        public static int DefaultLifetimeDays = 3650; 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
        public abstract Product ProduceProduct();
    }
}
