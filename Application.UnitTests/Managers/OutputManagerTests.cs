using NUnit.Framework;
using Application.Entities;
using Application.Managers;
using System;
using System.Collections.Generic;

namespace Application.UnitTests.Managers
{
    [TestFixture]
    public class OutputManagerTests
    {
        private OutputManager _outputManager;

        [SetUp]
        public void SetUp()
        {
            _outputManager = new OutputManager();
        }

        [Test]
        public void GenerateOutput_ValidOrder_ReturnsFormattedOutput()
        {
            // Arrange
            var order = new Order();
            order.Dishes.Add(new MenuPosition { Daytime = Daytime.Morning,  DishNumber = 1, DishType = DishType.Side, DishName = "Potato" }, 2);
            order.Dishes.Add(new MenuPosition { Daytime = Daytime.Morning,  DishNumber = 2, DishType = DishType.Drink, DishName = "Coffee" }, 1);
            order.Dishes.Add(new MenuPosition { Daytime = Daytime.Morning,  DishNumber = 3, DishType = DishType.Entree, DishName = "Egg" }, 1);
            var expectedOutput = "Egg,Potato(x2),Coffee";

            // Act
            var result = _outputManager.GenerateOutput(order);

            // Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void GenerateOutput_EmptyOrder_ReturnsEmptyString()
        {
            // Arrange
            var order = new Order();
            var expectedOutput = string.Empty;

            // Act
            var result = _outputManager.GenerateOutput(order);

            // Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void GenerateErrorMessage_Exception_ReturnsErrorMessage()
        {
            // Arrange
            var exception = new Exception("Some error");
            var expectedErrorMessage = "error";

            // Act
            var result = _outputManager.GenerateErrorMessage(exception);

            // Assert
            Assert.That(result, Is.EqualTo(expectedErrorMessage));
        }
    }
}