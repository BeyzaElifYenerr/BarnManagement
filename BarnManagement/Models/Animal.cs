using System;

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

        public int BarnId { get; set; }
        public virtual Barn Barn { get; set; }

        public static int DefaultLifetimeDays = 3650; 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
        public abstract Product ProduceProduct();
    }
}
