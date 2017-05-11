using System.Collections.Generic;

namespace Vending
{
    public class Coin
    {
        public static readonly Coin Fifty = new Coin(0.6m, 0.5m, 15, 14.5m, 0.5m);
        public static readonly Coin Twenty = new Coin(0.6m, 0.5m, 5, 4.5m, 0.2m);
        public static readonly Coin Ten = new Coin(0.2m, 0.1m, 10, 9, 0.1m);
        public static readonly Coin Five = new Coin(0.2m, 0.1m, 5, 4, 0.05m);

        //stay in this order
        public static readonly IReadOnlyList<Coin> Coins = new List<Coin>()
        {
            Coin.Fifty,
            Coin.Twenty,
            Coin.Ten,
            Coin.Five
        };


        public Coin(decimal maxWeight, decimal minWeight, decimal maxDiameter, decimal minDiameter, decimal value)
        {
            MaxWeight = maxWeight;
            MinWeight = minWeight;
            MaxDiameter = maxDiameter;
            MinDiameter = minDiameter;
            Value = value;
        }

        public decimal MaxWeight { get; }
        public decimal MinWeight { get; }
        public decimal MaxDiameter { get; }
        public decimal MinDiameter { get; }

        public decimal Value { get; }

        public override string ToString()
        {
            return $"Coin Value {Value}";
        }
    }
}