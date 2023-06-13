using Application.Entities;

namespace Application.Managers
{
    internal interface IInputManager
    {
        UserInput ParseUserInput(string userInput);
    }
}