using Application.Entities;
using Application.Managers;
using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("Application.UnitTests")]
namespace Application;

public class Server : IServer
{
    private readonly IInputManager _inputManager;
    private readonly IOrderManager _orderManager;
    private readonly IOutputManager _outputManager;

    public Server(ServerConfig config) 
    {
        var servicesCollection = SetUp.SetUpServiceCollection(config);
        _inputManager = servicesCollection.GetRequiredService<IInputManager>();
        _orderManager = servicesCollection.GetRequiredService<IOrderManager>();
        _outputManager = servicesCollection.GetRequiredService<IOutputManager>();
    }

    internal Server(IInputManager inputManager, IOutputManager outputManager, IOrderManager orderManager)
    {
        _inputManager = inputManager;
        _outputManager = outputManager;
        _orderManager = orderManager;
    }

    public async Task<string> TakeOrderAsync(string unparsedOrder)
    {
        try
        {
            var userParams = _inputManager.ParseUserInput(unparsedOrder);
            var order = await _orderManager.GenerateOrderAsync(userParams.Daytime, userParams.DishNumbers);
            return _outputManager.GenerateOutput(order);
        }
        catch
        {
            return "error";
        }
    }
}