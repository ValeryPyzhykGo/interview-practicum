using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;

namespace Application.Managers
{
    internal class OutputManager : IOutputManager
    {
        private const string ErrorMessage = "error";
        
        private readonly Dictionary<DishType, int> _outputOrder = new Dictionary<DishType, int>
        {
            { DishType.Entree,  0 },
            { DishType.Side,    1 },
            { DishType.Drink,   2 },
            { DishType.Dessert, 3 },
        };

        public string GenerateOutput(Order order)
        {
            var sortedOrder = order.Dishes.ToList().OrderBy((a) => _outputOrder[a.Key.DishType]);
            return string.Join(",", sortedOrder.Select(DishToString));
        }


        public string GenerateErrorMessage(Exception exception)
        {
            return ErrorMessage;
        }

        private string DishToString(KeyValuePair<MenuPosition, int> orderPosition)
        {
            return orderPosition.Key.DishName + 
                (orderPosition.Value > 1 ? $"(x{orderPosition.Value})" : string.Empty);
        }
    }
}
