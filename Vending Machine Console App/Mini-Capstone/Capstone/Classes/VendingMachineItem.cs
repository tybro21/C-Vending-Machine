using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class VendingMachineItem
    {
        public string SlotLocation { get; set; }
        public string ProductName { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }


        public override string ToString()
        {
            return $"{SlotLocation} {ProductName} {Cost}";

        }
    }
}
