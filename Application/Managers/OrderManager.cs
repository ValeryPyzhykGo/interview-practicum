using Application.Entities;
using Application.Exceptions;
using Application.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Managers;

internal class OrderManager : IOrderManager
{
    private readonly IMenuRepository _menuRepository;

    public OrderManager(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    /// <summary>
    ///     Constructs an order from user input.
    /// </summary>
    /// <param name="daytime"></param>
    /// <param name="listOfDishNumbers"></param>
    /// <returns></returns>
    public async Task<Order> GenerateOrderAsync(Daytime daytime, List<int> listOfDishNumbers)
    {
        var order = new Order();
        
        var menu = await _menuRepository.ReadMenuAsync();
        foreach (var dishNumber in listOfDishNumbers)
        {
            var menuPosition = menu.FirstOrDefault(x => x.Daytime == daytime && x.DishNumber == dishNumber);
            if (order.Dishes.ContainsKey(menuPosition))  
            {
                if (!menuPosition.IsMultiple)
                {
                    throw new ForbidenMultipleMenuPostionException(menuPosition.DishName);
                }
                order.Dishes[menuPosition] += 1;
            } 
            else
            {
                order.Dishes[menuPosition] = 1;
            }
        }

        return order;
    }
}