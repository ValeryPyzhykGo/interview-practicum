using Application.Entities;
using System;

namespace Application.Managers
{
    internal interface IOutputManager
    {
        string GenerateOutput(Order order);
        string GenerateErrorMessage(Exception exception);
    }
}