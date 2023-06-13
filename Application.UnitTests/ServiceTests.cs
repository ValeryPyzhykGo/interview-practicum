using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using Application.Managers;
using Application.Entities;

namespace Application.UnitTests
{
    [TestFixture]
    public class ServerTests
    {
        private Server _server;
        private Mock<IInputManager> _inputManagerMock;
        private Mock<IOutputManager> _outputManagerMock;
        private Mock<IOrderManager> _orderManagerMock;

        [SetUp]
        public void SetUp()
        {
            _inputManagerMock = new Mock<IInputManager>();
            _outputManagerMock = new Mock<IOutputManager>();
            _orderManagerMock = new Mock<IOrderManager>();

            _server = new Server(_inputManagerMock.Object, _outputManagerMock.Object, _orderManagerMock.Object);
        }

        [Test]
        public async Task TakeOrderAsync_ValidInput_ReturnsOutput()
        {
            // Arrange
            var unparsedOrder = "morning, 1, 2, 3";
            var parsedUserInput = new UserInput { Daytime = Daytime.Morning, DishNumbers = new List<int> { 1, 2, 3 } };
            var order = new Order();
            var expectedOutput = "output";

            _inputManagerMock.Setup(m => m.ParseUserInput(unparsedOrder)).Returns(parsedUserInput);
            _orderManagerMock.Setup(m => m.GenerateOrderAsync(parsedUserInput.Daytime, parsedUserInput.DishNumbers)).ReturnsAsync(order);
            _outputManagerMock.Setup(m => m.GenerateOutput(order)).Returns(expectedOutput);

            // Act
            var result = await _server.TakeOrderAsync(unparsedOrder);

            // Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public async Task TakeOrderAsync_InvalidInput_ReturnsError()
        {
            // Arrange
            var unparsedOrder = "invalid order";
            var expectedOutput = "error";

            _inputManagerMock.Setup(m => m.ParseUserInput(unparsedOrder)).Throws(new Exception());

            // Act
            var result = await _server.TakeOrderAsync(unparsedOrder);

            // Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }
    }
}