using Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Managers;

public interface IOrderManager
{
    /// <summary>
    ///     Constructs an order from user input
    /// </summary>
    /// <param name="daytime"></param>
    /// <param name="listOfDishNumbers"></param>
    /// <returns></returns>
    Task<Order> GenerateOrderAsync(Daytime daytime, List<int> listOfDishNumbers);
}