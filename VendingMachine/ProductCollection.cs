using System.Collections.Generic;

namespace VendingMachine
{
    public class ProductCollection
    {
        private Dictionary<Product, int> Products { get; set; }

        public ProductCollection()
        {
            Products = new Dictionary<Product, int>();
        }

        internal void Insert(Product product, int num)
        {
            if(Products.ContainsKey(product))
            {
                Products[product] += num;
            }
            else
            {
                Products.Add(product, num);
            }
        }

        internal void Dispense(Product product)
        {
            int count = Count(product);
            if(count > 0)
            {
                Products[product] = count - 1;
            }
        }

        public int Count(Product product)
        {
            int count = 1;
            if(Products.ContainsKey(product))
            {
                count = Products[product];
            }

            return count;
        }
    }
}
