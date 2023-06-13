using NUnit.Framework;
using Moq;
using Application.Entities;
using Application.Exceptions;
using Application.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Managers;

namespace Application.UnitTests.Managers
{
    [TestFixture]
    public class OrderManagerTests
    {
        private OrderManager _orderManager;
        private Mock<IMenuRepository> _menuRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _menuRepositoryMock = new Mock<IMenuRepository>();
            _orderManager = new OrderManager(_menuRepositoryMock.Object);
        }

        [Test]
        public async Task GenerateOrderAsync_ValidInput_ReturnsOrder()
        {
            // Arrange
            var daytime = Daytime.Morning;
            var dishNumbers = new List<int> { 1, 2, 3 };
            var menu = new List<MenuPosition>
            {
                new MenuPosition { Daytime = Daytime.Morning, DishNumber = 1 },
                new MenuPosition { Daytime = Daytime.Morning, DishNumber = 2 },
                new MenuPosition { Daytime = Daytime.Morning, DishNumber = 3 }
            };
            var expectedOreder = new Order();
            expectedOreder.Dishes.Add(new MenuPosition { Daytime = Daytime.Morning, DishNumber = 1 }, 1);
            expectedOreder.Dishes.Add(new MenuPosition { Daytime = Daytime.Morning, DishNumber = 2 }, 1);
            expectedOreder.Dishes.Add(new MenuPosition { Daytime = Daytime.Morning, DishNumber = 3 }, 1);

            _menuRepositoryMock.Setup(m => m.ReadMenuAsync()).ReturnsAsync(menu);

            // Act
            var result = await _orderManager.GenerateOrderAsync(daytime, dishNumbers);

            // Assert
            CollectionAssert.AreEqual(result.Dishes.ToList(), expectedOreder.Dishes.ToList());
        }

        [Test]
        public async Task GenerateOrderAsync_InvalidInput_ThrowsForbiddenMultipleMenuPositionException()
        {
            // Arrange
            var daytime = Daytime.Morning;
            var dishNumbers = new List<int> { 1, 2, 2 }; // Duplicate dish number
            var menu = new List<MenuPosition>
            {
                new MenuPosition { Daytime = Daytime.Morning, DishNumber = 1 },
                new MenuPosition { Daytime = Daytime.Morning, DishNumber = 2 }
            };

            _menuRepositoryMock.Setup(m => m.ReadMenuAsync()).ReturnsAsync(menu);

            // Act & Assert
            Assert.ThrowsAsync<ForbidenMultipleMenuPostionException>(async () =>
                await _orderManager.GenerateOrderAsync(daytime, dishNumbers));
        }
    }
}