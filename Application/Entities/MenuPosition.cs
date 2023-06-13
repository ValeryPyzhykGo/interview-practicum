using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Entities
{
    public class MenuPosition
    {
        public Daytime Daytime { get; set; }

        public int DishNumber { get; set; }

        public DishType DishType { get; set; }

        public string DishName { get; set; }

        public bool IsMultiple { get; set; }

        // This two methods are needed to use this class as key fro dictionary.
        public override int GetHashCode()
        {   
            const int primeToCreateWellDistributedHash = 17;
            return Daytime.GetHashCode() * primeToCreateWellDistributedHash + DishNumber.GetHashCode();   
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is MenuPosition))
                return false;

            MenuPosition other = (MenuPosition)obj;
            return Daytime == other.Daytime && DishNumber == other.DishNumber;
        }

    }
}
