using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void FeedMoneyTest()
        {
            VendingMachine vm = new VendingMachine();
            vm.FeedMoney(5);
            Assert.AreEqual(vm.moneyAvailable,5);

            vm.FeedMoney(1);
            Assert.AreEqual(vm.moneyAvailable, 6);

            vm.FeedMoney(0);
            Assert.AreEqual(vm.moneyAvailable, 6);

            vm.FeedMoney(13);
            Assert.AreEqual(vm.moneyAvailable, 6);
        }
    }
}
