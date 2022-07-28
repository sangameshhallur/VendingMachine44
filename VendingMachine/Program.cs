using System;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachineClass vm = new VendingMachineClass();
            vm.insertCoins();
            vm.selectProduct();
            Console.WriteLine("Thank You....");
            Console.ReadLine();
        }
    }
}
