using Application;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ApplicationTests.AcceptanceTests;

[TestFixture]
public class ServerTests
{
    private Server _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new Server(new ServerConfig()
        {
            MenuFilePath = "menu.csv"
        });
    }

    [TestCase("morning, 1, 2, 3", "egg,toast,coffee")]
    [TestCase("Morning,3,3,3", "coffee(x3)")]
    [TestCase("morning ,1,3,2,3", "egg,toast,coffee(x2)")]
    [TestCase("evening,1, 2, 3, 4", "steak,potato,wine,cake")]
    [TestCase("Evening,1, 2, 2, 4", "steak,potato(x2),cake")]
    public async Task TakeOrder_CorrectInput_ReturnsOrederString(string userInput, string expectedResult)
    {
        // Act
        var actual = await _sut.TakeOrderAsync(userInput);

        // Assert
        Assert.AreEqual(expectedResult, actual);
    }

    [TestCase("morning, 1, 2, 2", "error")]
    [TestCase("morning, 1, 2, 4", "error")]
    [TestCase("evening,1, 2, 3, 5", "error")]
    [TestCase("evening,1, 3, 2, 3", "error")]
    public async Task TakeOrder_IncorrectInput_ReturnsErrorMessage(string userInput, string expectedResult)
    {
        // Act
        var actual = await _sut.TakeOrderAsync(userInput);
       
        // Assert
        Assert.AreEqual(expectedResult, actual);
    }
}