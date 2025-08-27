using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarnManagement.Models
{
    public static class Enums
    {
        public enum Gender { Male, Female }
        public enum Species { Cow, Chicken, Sheep }
        public enum ProductType { Milk, Egg, Wool, Other = 99 }
    }
}
