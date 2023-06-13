using Application.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Managers
{
    internal class InputManager : IInputManager
    {
        private readonly IMapper _mapper;

        public InputManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UserInput ParseUserInput(string userInput)
        {
            var words = userInput.Split(',').Select(x => x.Trim()).ToList();

            if (words.Count < 2)
            {
                throw new ArgumentException("Invalid user input format.");
            }

            List<int> menuPositions;
            try
            {
                return new UserInput()
                {
                    Daytime = _mapper.Map<Daytime>(words[0]),
                    DishNumbers = menuPositions = words.Skip(1).Select(int.Parse).ToList()
                };
            }
            catch
            {
                throw new ArgumentException("Invalid user input format.");
            }
        }
    }
}
