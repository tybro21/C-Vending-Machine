using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        private List<VendingMachineItem> items = new List<VendingMachineItem>();

        VendingMachineItem vm = new VendingMachineItem();
        private string filePath = @"C:\VendingMachine";
        private string fileName = "vendingmachine.csv";

        public decimal moneyAvailable = 0.00M;

        public bool ReadFile()
        {
            bool result = true;

            try
            {
                string path = Path.Combine(filePath, fileName);
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string nextString = sr.ReadLine();
                        string[] splitString = nextString.Split('|');

                        VendingMachineItem vm = new VendingMachineItem();
                        vm.SlotLocation = splitString[0];
                        vm.ProductName = splitString[1];
                        vm.Cost = decimal.Parse(splitString[2]);
                        vm.Quantity = 5;
                        items.Add(vm);
                    }

                }

            }
            catch (Exception ex)
            {
                result = false;
            }


            return result;
        }

        public VendingMachineItem[] List()
        {
            return items.ToArray();
        }


        public void FeedMoney(decimal moneyIn)
        {
            bool done = false;

            while (!done)
            {
                switch (moneyIn)
                {
                    case 0:
                        done = true;
                        break;
                    case 1:
                        moneyAvailable += 1.00M;
                        done = true;
                        break;
                    case 2:
                        moneyAvailable += 2.00M;
                        done = true;
                        break;
                    case 5:
                        moneyAvailable += 5.00M;
                        done = true;
                        break;
                    case 10:
                        moneyAvailable += 10.00M;
                        done = true;
                        break;
                    default:

                        Console.WriteLine("Please enter a valid amount");
                        done = true;
                        break;
                }
            }
            string directory = @"C:\VendingMachine\";
            string filename = "Log.txt";
            string fullPath = Path.Combine(directory, filename);

            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {
                sw.WriteLine($"{DateTime.UtcNow} FEED MONEY:             ${moneyIn,-6}     ${moneyAvailable,-6}");
            }
        }


        

        public void ProductSelect()
        {
            VendingMachineItem[] result = List();

            foreach (VendingMachineItem item in result)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("Pleae enter slot location: ");
            string slotCode = Console.ReadLine();
            slotCode = slotCode.ToUpper();

            try
            {
                foreach (VendingMachineItem item in items)
                {
                    if (slotCode == item.SlotLocation && item.Quantity > 0 && moneyAvailable >= item.Cost)
                    {
                        string directory = @"C:\VendingMachine\";
                        string filename = "Log.txt";
                        string fullPath = Path.Combine(directory, filename);

                        using (StreamWriter sw = new StreamWriter(fullPath, true))
                        {
                            sw.WriteLine($"{DateTime.UtcNow} {item.ProductName,-18} {item.SlotLocation}   ${moneyAvailable,-6}     ${moneyAvailable - item.Cost,-6}");   
                        }
                        moneyAvailable -= item.Cost;
                        item.Quantity -= 1;

                        switch (slotCode.Substring(0, 1))
                        {
                            case "A":
                                Console.WriteLine("Crunch Crunch, Yum!");
                                Console.WriteLine();
                                break;

                            case "B":
                                Console.WriteLine("Munch Munch, Yum!");
                                Console.WriteLine();
                                break;

                            case "C":
                                Console.WriteLine("Glug Glug, Yum!");
                                Console.WriteLine();
                                break;

                            case "D":
                                Console.WriteLine("Chew Chew, Yum!");
                                Console.WriteLine();
                                break;
                        }
                       
                    }

                    else if (slotCode == item.SlotLocation && moneyAvailable < item.Cost)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please deposit money to purchase an item.");
                        Console.WriteLine();
                        
                    }

                    else if (item.Quantity == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Item is Sold out. Please make another");
                        Console.WriteLine();
                        ProductSelect();
                    }
                }

            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("Please make a valid selection.");
                Console.WriteLine();
                ProductSelect();
            }

            
        }

        public int quarter;
        public int dime;
        public int nickel;

        public void ChangeBack()
        {
            Console.WriteLine("Your change due is:   $" + moneyAvailable);

            decimal changeDue = moneyAvailable;
            while (moneyAvailable > 0)
            {
                if (moneyAvailable > 0.25M)
                {
                    quarter++;
                    moneyAvailable -= 0.25M;
                }
                else if (moneyAvailable > 0.10M)
                {
                    dime++;
                    moneyAvailable -= 0.10M;
                }
                else if (moneyAvailable >= 0.05M)
                {
                    nickel++;
                    moneyAvailable -= 0.05M;
                }
            }
            Console.WriteLine($"Your change back is {quarter} Quarter(s), {dime} Dime(s), and {nickel} Nickel(s).");
            Console.WriteLine();


            string directory = @"C:\VendingMachine\";
            string filename = "Log.txt";
            string fullPath = Path.Combine(directory, filename);

            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {

                sw.WriteLine($"{DateTime.UtcNow} GIVE CHANGE:            ${changeDue,-6}     ${moneyAvailable,-6}");
            }
        }
    }
    
}

