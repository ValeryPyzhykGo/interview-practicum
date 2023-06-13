using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Entities
{
    public enum DishType
    {
        [Display(Name = "entrée")]
        Entree = 1,

        [Display(Name = "side")]
        Side = 2,

        [Display(Name = "drink")]
        Drink = 3,

        [Display(Name = "dessert")]
        Dessert = 4
    }
}
