using NUnit.Framework;
using AutoMapper;
using Application.Entities;
using Application.Managers;
using System;

namespace Application.UnitTests.Managers
{
    [TestFixture]
    public class InputManagerTests
    {
        private InputManager _inputManager;

        [SetUp]
        public void SetUp()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<string, Daytime>().ConvertUsing(s => Enum.Parse<Daytime>(s, true));
            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            _inputManager = new InputManager(mapper);
        }

        [Test]
        public void ParseUserInput_ValidInput_ReturnsUserInput()
        {
            // Arrange
            var userInput = "morning, 1, 2, 3";
            var expectedUserInput = new UserInput
            {
                Daytime = Daytime.Morning,
                DishNumbers = new List<int> { 1, 2, 3 }
            };

            // Act
            var result = _inputManager.ParseUserInput(userInput);

            // Assert
            Assert.That(result.Daytime, Is.EqualTo(expectedUserInput.Daytime));
            CollectionAssert.AreEqual(result.DishNumbers, expectedUserInput.DishNumbers);
        }

        [Test]
        public void ParseUserInput_InvalidInput_ThrowsArgumentException()
        {
            // Arrange
            var userInput = "morning"; // Invalid input format

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _inputManager.ParseUserInput(userInput));
        }
    }
}