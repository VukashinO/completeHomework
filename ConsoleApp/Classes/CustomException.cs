using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Classes
{
    public class CustomException : Exception
    {
        public void PrintError()
        {
            Console.WriteLine("you have entered invalid input! Please try again Y/n");
        }
    }
}
