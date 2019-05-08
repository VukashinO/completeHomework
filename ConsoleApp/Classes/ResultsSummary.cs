using ConsoleApp;
using ConsoleApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Entities.Classes
{
    public class ResultsSummary
    {
        public int UserInput;
        public int Min;
        public int Max;
        public string TypeInput;
        public double TotalCost;
        public int Reciept;
        public bool SMS;
        public bool EMail;
        public bool MailPost;


        public List<Item> Cart;
        public List<Part> Parts = Db.Parts;
        public List<Module> Modules = Db.Modules;
        public List<Configuration> Configurations = Db.Configurations;

        public ResultsSummary()
        {
            Cart = new List<Item>();
        }

        public void ShowPMC<T>(IEnumerable<T> list) where T : Item
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Select a part:");
            Console.ResetColor();
           
            foreach (T item in list)
            {
                item.PrintInfo();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }



        public void AddToCart(int answerId)
        {
            if (int.TryParse(Console.ReadLine(), out UserInput))
            {
                switch (answerId)
                {
                    case 1:
                        if (UserInput > 0 && UserInput <= Parts.Count)
                        {
                            if (Cart.Where(x => x.Id > 0 && x.Id < 46).ToList().Count < 10) Cart.Add(Parts[UserInput - 1]);
                            else
                            {
                                Console.WriteLine("You have to many Parts in your cart");
                                Console.ReadKey();
                            }
                        }
                        else throw new CustomException();
                        break;
                    case 2:
                        if (UserInput > 0 && UserInput <= Modules.Count)
                        {
                            if (Cart.Where(x => x.Id > 49 && x.Id < 61).ToList().Count < 5) Cart.Add(Modules[UserInput - 1]);
                            else
                            {
                                Console.WriteLine("You have to many Modules in your cart");
                                Console.ReadKey();
                            }
                        }
                        else throw new CustomException();
                        break;
                    case 3:
                        if (UserInput > 0 && UserInput <= Configurations.Count)
                        {
                            if (Cart.Where(x => x.Id > 79 && x.Id < 84).ToList().Count < 1) Cart.Add(Configurations[UserInput - 1]);
                            else
                            {
                                Console.WriteLine("You already have a configuration in your cart");
                                Console.ReadKey();
                            }
                        }
                        else throw new CustomException();
                        break;
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The item  was  succsessfully added to your cart, you currently have {Cart.Count} items in your cart");
                Console.ResetColor();
                Console.WriteLine("Select an Option:");
                Console.WriteLine("1) Continue shopping");
                Console.WriteLine("2) Choose something else");
                Console.WriteLine("3) See cart");
                Console.WriteLine("4) Continue to check out");

                if (int.TryParse(Console.ReadLine(), out UserInput))
                {
                    switch (UserInput)
                    {
                        case 1:
                            switch (answerId)
                            {
                                case 1:
                                    ShowPMC(Parts);
                                    AddToCart(1);
                                    break;
                                case 2:
                                    ShowPMC(Modules);
                                    AddToCart(2);
                                    break;
                                case 3:
                                    ShowPMC(Configurations);
                                    AddToCart(3);
                                    break;
                            }
                            break;
                        case 2:
                            UserInterface();
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine("items in your cart:");
                            CheckCart();               
                            Console.ReadKey();
                            UserInterface();
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine("items in your cart:");
                            CheckCart();
                            Console.WriteLine("how would u like the reciept:\n1.SMS\n2.E-Mail\n3.Mail ( Post )\n4.Continue");
                            CheckOut();
                            Console.ReadKey();
                            break;
                    }
                }
            }
            else throw new CustomException();
        }



        private void CheckCart()
        {
            foreach (Item cartItem in Cart)
            {
                if (cartItem.Id < 46)
                {
                    foreach (Part part in Parts)
                    {
                        if (cartItem.Id == part.Id)
                Console.WriteLine($"Part, ID: {part.Id}, Name: {part.Name}, " +
                    $"Type: {part.Type}, Company: {part.Company}," +
                    $" Quantity: {part.Quantity}, Price(10% discount): {part.Price - part.Price / 10}$," +
                    $" Warranty: {part.Warranty}");
                    }
                }
                else if (cartItem.Id > 49 && cartItem.Id < 61)
                {
                    foreach (Module module in Modules)
                    {
                        if (cartItem.Id == module.Id)
                            Console.WriteLine($"Module, ID: {module.Id}," +
                                $" Type: {module.Type}, Price(10% discount): {module.Price - module.Price / 10}$");
                    }
                }
                else if (cartItem.Id > 79 && cartItem.Id < 84)
                {
                    foreach (Configuration config in Configurations)
                    {
                        if (cartItem.Id == config.Id)
                            Console.WriteLine($"Configuration, ID: {config.Id}, " +
                                $"Title: {config.Title}, Type: {config.Type}," +
                                $" Price(10% discount): {config.Price - config.Price / 10}$");
                    }
                }
                TotalCost += cartItem.Price;
            }
            Console.WriteLine($"Number of Items: {Cart.Count}, Total Price: {TotalCost - TotalCost / 10}$, Money saved with discount: {TotalCost / 10}$");
        }



        public void CheckOut()
        {
            if (int.TryParse(Console.ReadLine(), out Reciept) && Reciept > 0 && Reciept < 5)
            {
                switch (Reciept)
                {
                    case 1:
                        SMS = true;
                        Console.WriteLine("we will send you Reciept through SMS");
                        Console.WriteLine("press 4 after confirm Reciept so we can proceed");
                        CheckOut();
                        break;
                    case 2:
                        EMail = true;
                        Console.WriteLine("we will send you Reciept through EMail");
                        Console.WriteLine("press 4 after confirm Reciept so we can proceed");
                        CheckOut();
                        break;
                    case 3:
                        MailPost = true;
                        Console.WriteLine("we will send you Reciept through MailPost");
                        Console.WriteLine("press 4 after confirm Reciept so we can proceed");
                        CheckOut();
                        break;
                    case 4:
                        if (SMS || EMail || MailPost)
                            SendReciept();
                        else
                        {
                            Console.WriteLine("please check if u have entered valid integer!");
                            CheckOut();
                        }
                        break;
                }
            }
        }


        public void SendReciept()
        {
            Console.WriteLine("Loading...");
            Thread.Sleep(3000);
            if (SMS) Console.WriteLine("a message got recieved through SMS");
            if (EMail) Console.WriteLine("a message got recieved through EMail");
            if (MailPost) Console.WriteLine(" a message got recieved through MailPost");
            SMS = false;
            EMail = false;
            MailPost = false;
            Cart = new List<Item>();
            TotalCost = 0;
            Console.WriteLine("do u want to continue Y/n");
            if(Console.ReadLine() != "y")
            {
                throw new CancelApp();
            }
        }
        

        public void UserInterface()
        {
            Console.Clear();
            Console.WriteLine("Please choose an option:" +
                " \n1.Parts\n2.Modules\n3.Configurations");

            if (int.TryParse(Console.ReadLine(), out UserInput))
            {
                switch (UserInput)
                {
                case 1:                                                 
                Console.Clear();
                Console.WriteLine("Select An Option:");                 
                Console.WriteLine("1) All Products");
                Console.WriteLine("2) ByPrice");
                Console.WriteLine("3) By Type");
        if (int.TryParse(Console.ReadLine(), out UserInput))
        {
            switch (UserInput)
            {
                case 1:
                    ShowPMC(Parts);
                     AddToCart(1);
                    break;
                case 2:
                    Console.WriteLine("Enter minimum price(8$):");
                    if (int.TryParse(Console.ReadLine(), out Min) && Min >= 8)
                    {
                        Console.WriteLine("Enter maximum price:");
                        if (int.TryParse(Console.ReadLine(), out Max) && Max >= Min)
                        {
                            ShowPMC(Parts.Where(x => x.Price >= Min && x.Price <= Max));
                                            Console.ReadLine();
                            AddToCart(1);
                        }
                        else throw new CustomException();
                    }
                    else
                    {
                        Console.WriteLine("Lowest Price of a part is 8$");
                        Console.ReadKey();
                        UserInterface();
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter part type(Proccessing,Graphics,Casing,MainBoard,Memory,Other)");
                       TypeInput = Console.ReadLine();
                   switch (TypeInput.ToLower())
                   {
                       case "proccessing":
                    ShowPMC(Parts.Where(x => x.Type == PartType.Cpu || x.Type == PartType.CpuCooler));
                           break;
                       case "graphics":
                    ShowPMC(Parts.Where(x => x.Type == PartType.Gpu || x.Type == PartType.GpuCooler));
                                           
                           break;
                       case "casing":
                    ShowPMC(Parts.Where(x => x.Type == PartType.Case || x.Type == PartType.PowerSuply));
                           break;
                       case "mainboard":
                    ShowPMC(Parts.Where(x => x.Type == PartType.MotherBoard || x.Type == PartType.ConnectionCable || x.Type == PartType.PowerCable));
                           break;
                       case "memory":
                    ShowPMC(Parts.Where(x => x.Type == PartType.SSD || x.Type == PartType.HDD || x.Type == PartType.RAM));
                           break;
                       case "other":
                    ShowPMC(Parts.Where(x => x.Type == PartType.Monitor || x.Type == PartType.Mouse || x.Type == PartType.Keyboard));
                           break;
                       default:
                           throw new CustomException();
                        
                   }
                     AddToCart(1);
                    break;
                default:
                    Console.WriteLine("you need to choose a number between 1 and 3!");
                    Console.ReadKey();
                    UserInterface();
                    break;
            }
        }
                        else throw new CustomException();
                        break;
                    case 2:                                            
                        Console.Clear();
                        Console.WriteLine("Select An Option:");
                        Console.WriteLine("1) All Products");
                        Console.WriteLine("2) ByPrice");
                        Console.WriteLine("3) By Type");                     
                        if (int.TryParse(Console.ReadLine(), out UserInput))
                        {
                            switch (UserInput)
                            {
                                case 1:
                                    ShowPMC(Modules);
                                    AddToCart(2);
                                    break;
                                case 2:
                              Console.WriteLine("Enter minimum price(100$):");
                              if (int.TryParse(Console.ReadLine(), out Min) && Min >= 100)
                              {
                                  Console.WriteLine("Enter maximum price:");
                                  if (int.TryParse(Console.ReadLine(), out Max) && Max >= Min)
                                  {
                              ShowPMC(Modules.Where(x => x.Price >= Min && x.Price <= Max));
                                      AddToCart(2);
                                  }
                                  else throw new CustomException();
                                    }
                                    else
                                    {
                                        Console.WriteLine("The Lowest Price of a module is 100$");
                                        Console.ReadKey();
                                        UserInterface();
                                    }
                                    break;
                                case 3:
                                   Console.WriteLine("Enter Module type(Proccessing,Graphics,Casing,MainBoard,Memory,Other)");
                                    TypeInput = Console.ReadLine();
                                   switch (TypeInput.ToLower())
                                   {
                                       case "proccessing":
                                    ShowPMC(Modules.Where(x => x.Type == ModuleType.Processing));
                                           break;
                                       case "graphics":
                                   ShowPMC(Modules.Where(x => x.Type == ModuleType.Graphics));
                                           break;
                                       case "casing":
                                   ShowPMC(Modules.Where(x => x.Type == ModuleType.Casing));
                                           break;
                                       case "mainboard":
                                   ShowPMC(Modules.Where(x => x.Type == ModuleType.MainBoard));
                                           break;
                                       case "memory":
                                   ShowPMC(Modules.Where(x => x.Type == ModuleType.Memory));
                                           break;
                                       case "other":
                                   ShowPMC(Modules.Where(x => x.Type == ModuleType.Other));
                                           break;
                                       default:
                                           throw new CustomException();
                                           
                                   }
                                   AddToCart(2);
                                    break;
                                default:
                                    Console.WriteLine("you need to choose a number between 1 and 3!");
                                    Console.ReadKey();
                                    UserInterface();
                                    break;
                            }
                        }
                        else throw new CustomException();
                        break;
                    case 3:                                                  
                        Console.Clear();
                        Console.WriteLine("Select An Option:");
                        Console.WriteLine("1) All Products");
                        Console.WriteLine("2) ByPrice");
                        Console.WriteLine("3) By Type");
                        if (int.TryParse(Console.ReadLine(), out UserInput))
                        {
                            switch (UserInput)
                            {
                                case 1:
                                    ShowPMC(Configurations);
                                    AddToCart(3);
                                    break;
                                case 2:
                                Console.WriteLine("Enter minimum price(800$):");
                                if (int.TryParse(Console.ReadLine(), out Min) && Min >= 800)
                                {
                                    Console.WriteLine("Enter maximum price:");
                                    if (int.TryParse(Console.ReadLine(), out Max) && Max >= Min)
                                    {
                                       ShowPMC(Configurations.Where(x => x.Price >= Min && x.Price <= Max));
                                        AddToCart(3);
                                    }
                                    else throw new CustomException();
                                }
                                else
                                {
                                    Console.WriteLine("The Lowest Price of a Configuration is 800$");
                                    Console.ReadKey();
                                    UserInterface();
                                }
                                    break;
                                case 3:
                                 Console.WriteLine("Enter Configuration type(Standard,Office,Gaming)");
                                 TypeInput = Console.ReadLine();
                                 switch (TypeInput.ToLower())
                                 {
                                     case "standard":
                                    ShowPMC(Configurations.Where(x => x.Type == ConfigurationType.Standard));
                                         break;
                                     case "office":
                                  ShowPMC(Configurations.Where(x => x.Type == ConfigurationType.Office));
                                         break;
                                     case "gaming":
                                 ShowPMC(Configurations.Where(x => x.Type == ConfigurationType.Gaming));
                                         break;
                                     default:
                                         throw new CustomException();
                                         
                                 }
                                    AddToCart(3);
                                    break;
                                default:
                                    Console.WriteLine("you need to choose a number between 1 and 3!");
                                    Console.ReadKey();
                                    UserInterface();
                                    break;
                            }
                        }
                        else throw new CustomException();
                        break;
                    default:
                        Console.WriteLine("you need to choose a number between 1 and 3!");
                        Console.ReadKey();
                        UserInterface();
                        break;
                }
            }
            else throw new CustomException();


        }

    }
}
