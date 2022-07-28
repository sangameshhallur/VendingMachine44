
using System;

namespace VendingMachine
{
    public class VendingMachineClass
    {
        public VendingMachineClass()
        {
            CoinSlot = new CoinCollection();
            CoinReturn = new CoinCollection();
            CoinBank = new CoinCollection();
            Inventory = new ProductCollection();
        }

        
        public CoinCollection CoinSlot { get; private set; }

        public CoinCollection CoinReturn { get; private set; }

        public CoinCollection CoinBank { get; private set; }

        public ProductCollection Inventory { get; private set; }

        public void Insert(Coin coin, int num)
        {
            if (coin == Coin.PENNIE)
            {
                CoinReturn.Insert(coin, num);
            }
            else
            {
                CoinSlot.Insert(coin, num);
            }
        }
        public void Insert(Product product, int num)
        {
            Inventory.Insert(product, num);
        }
        public void AddToBank(Coin coin, int num)
        {
            CoinBank.Insert(coin, num);
        }
        public void Dispense(Product product)
        {
            int price = GetPrice(product);
            if (CoinSlot.Value >= price)
            {
                Inventory.Dispense(product);
                Console.WriteLine("Please Collect Your Product: " + product);
                int change = CoinSlot.Value - price;
                double changeDue = (double)change / 100;
                if (changeDue > 0)
                {                    
                    CoinBank.DispenseInto(CoinReturn, changeDue);
                    ReturnCoins(changeDue);
                }
                CoinSlot.EmptyInto(CoinBank);
            }
            else
            {                
                Console.WriteLine("Insufficient Coins, Please add some more coins to purchase the product");
                Console.WriteLine("----------------------------------------");
                insertCoins();
                selectProduct();
            }
            
        }

        public void ReturnCoins(double changeDue)
        {
            Console.WriteLine("Please collect your change: $" + changeDue);
            CoinSlot.EmptyInto(CoinReturn);
        }

        public int GetPrice(Product product)
        {
            int price = 0;
            if (product == Product.COLA)
            {
                price = 100;
            }
            else if(product == Product.CHIPS)
            {
                price = 50;
            }
            else if(product == Product.CANDY)
            {
                price = 65;
            }

            return price;
        }

        private string CreateCurrencyString(int amount)
        {
            return string.Format("{0:C}", amount / 100.0);
        }

        public void insertCoins()
        {
            Console.WriteLine("Please insert the coins separated by comma(,) and then press Enter. Example-nickel,dime,quarter");
            Array enumCoinArray = Enum.GetValues(typeof(Coin));
            double enumVal;
            foreach (int enumValue in enumCoinArray)
            {
                enumVal = (double)enumValue / 100;
                Console.WriteLine(Enum.GetName(typeof(Coin), enumValue) + " - $" + enumVal);
            }
            Console.WriteLine("----------------------------------------");
            string value = Console.ReadLine();
            string[] values = value.Split(",");
            Coin coin;

            for (int i = 0; i < values.Length; i++)
            {
                Enum.TryParse<Coin>(values[i].ToUpper(), out coin);
                Insert(coin, 1);
                if (coin != Coin.PENNIE)
                {
                    AddToBank(coin, 1);
                }
                else
                {
                    Console.WriteLine("You have inserted invalid coin PENNIE, Please collect it from return tray");
                }
                
            }
        }

        public void selectProduct()
        {
            double coinValue = (double)CoinSlot.Value / 100;
            if (coinValue > 0)
            {
                Console.WriteLine("Total value of coin inserted is: $" + coinValue);
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Please select the product");
                Array enumProductArray = Enum.GetValues(typeof(Product));
                double enumVal;
                foreach (int enumValue in enumProductArray)
                {
                    enumVal = (double)enumValue / 100;
                    Console.WriteLine(Enum.GetName(typeof(Product), enumValue) + " - $" + enumVal);
                }
                Console.WriteLine("----------------------------------------");
                string prod = Console.ReadLine();
                Product product;
                foreach (var item in Enum.GetNames(typeof(Product)))
                {
                    if (Enum.TryParse<Product>(prod.ToUpper(), out product))
                    {
                        Dispense(product);
                        break;
                    }
                }
            }
        }
    }
}
