using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarnManagement.Models
{
    public class Barn
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public int CurrentAnimalCount { get; set; }
        public decimal Balance { get; set; } 

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
