using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Classes
{
    public class CancelApp : Exception
    {
        public void Cancel()
        {
            Console.WriteLine("you have been succsesfully logged out");
        }
    }
}
