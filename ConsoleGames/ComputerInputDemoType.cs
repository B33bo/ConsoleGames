using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEngine.Demos;

namespace ConsoleGames
{
    public class ComputerInputDemoType : Demo.DemoInputType
    {
        public string Input;

        public ComputerInputDemoType(string input) =>
                Input = input;

        public ComputerInputDemoType(char input) =>
                Input = input.ToString();

        public ComputerInputDemoType(string[] args) =>
            Input = args[1];

        public override void OnCalled()
        {

        }

        public override string ToString()
        {
            return "compinput," + Input;
        }
    }
}
