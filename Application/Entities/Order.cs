using System.Collections.Generic;

namespace Application.Entities;

public class Order
{
    public readonly Dictionary<MenuPosition, int> Dishes = new Dictionary<MenuPosition, int>();
}