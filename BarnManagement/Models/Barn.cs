using System;

namespace BarnManagement.Models
{
    public class Barn
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public int CurrentAnimalCount { get; set; }
        public decimal Balance { get; set; }

        public int? OwnerUserId { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
