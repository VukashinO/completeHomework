using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Module : Item
    {
        public ModuleType Type { get; set; }
        public List<Part> Parts { get; private set; }
        public Module()
        {
            Parts = new List<Part>();
        }
        public Module(int id, double price, double discount, ModuleType type, List<Part> parts) 
            : base(id, price, discount)
        {
            Type = type;
            Parts = parts;
        }

        public override void PrintInfo()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Id: {Id}, Type:{Type}, Price: {Price}");
            Console.ResetColor();
            Console.WriteLine("Parts:");    
            foreach (Part part in Parts)
            {
                part.PrintInfo();
               
            }
            Console.WriteLine("-------------------------------------------------");
        }
    }
}
