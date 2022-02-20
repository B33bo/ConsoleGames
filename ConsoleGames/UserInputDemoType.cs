using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine.Demos;

namespace ConsoleGames
{
    public class UserInputDemoType : Demo.DemoInputType
    {
        public string Input;

        public UserInputDemoType(string input) =>
                Input = input;

        public UserInputDemoType(char input) =>
                Input = input.ToString();

        public UserInputDemoType(string[] args) =>
            Input = args[1];

        public override void OnCalled()
        {

        }

        public override string ToString()
        {
            return "userinput," + Input;
        }
    }
}
