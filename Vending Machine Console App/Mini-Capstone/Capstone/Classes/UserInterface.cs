using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UserInterface
    {
        private VendingMachine vendingMachine = new VendingMachine();
        
        public void RunInterface()
        {
            vendingMachine.ReadFile();
            bool done = false;
            while (!done)
            {
                Console.WriteLine();
                Console.WriteLine("(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase");
                Console.WriteLine("(3) End");

                Console.WriteLine();

                int choice = Int32.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        DisplayItemInfo();
                        break;

                    case 2:
                        PurchaseInterface();
                        break;

                    case 3:
                        done = true;
                        break;
                }
            }
        }

        public void DisplayItemInfo()
        {
            VendingMachineItem[] result = vendingMachine.List();

            foreach (VendingMachineItem item in result)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void PurchaseInterface()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("(1) Feed Money");
                Console.WriteLine("(2) Select Product");
                Console.WriteLine("(3) Finish Transaction");

                Console.WriteLine("Current Money Provided:   $" + vendingMachine.moneyAvailable);

                Console.WriteLine();

                int choice = Int32.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Please enter $(1), (2), (5), (10) or (0) to cancel");
                        decimal moneyIn = decimal.Parse(Console.ReadLine());
                        vendingMachine.FeedMoney(moneyIn + 0.00M);
                        break;

                    case 2:
                        vendingMachine.ProductSelect();
                        break;

                    case 3:
                        vendingMachine.ChangeBack();
                        done = true;
                        break;
                }
            }
        }
    }
}
