using System.Collections.Generic;

namespace Vending.Domain
{
    public class Product
    {
        public static readonly Product Cola = new Product("Cola", 1.0m);
        public static readonly Product Chips = new Product("Cola", 0.5m);
        public static readonly Product Candy = new Product("Candy", 0.65m);

        public static readonly IReadOnlyList<Product> Products = new List<Product>()
        {
            Cola,
            Chips,
            Candy
        };

        private Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; }
        public decimal Price { get; }

        public override string ToString()
        {
            return $"{Name} - {Price}";
        }
    }
}