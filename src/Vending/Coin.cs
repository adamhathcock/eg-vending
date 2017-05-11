namespace Vending
{
    public class Coin
    {
        public static readonly Coin Fifty = new Coin(0.6, 0.5, 15, 14.5, 0.5);
        public static readonly Coin Twenty = new Coin(0.6, 0.5, 5, 4.5, 0.2);
        public static readonly Coin Ten = new Coin(0.2, 0.1, 10, 9, 0.1);
        public static readonly Coin Five = new Coin(0.2, 0.1, 5, 4, 0.05);

        public Coin(double maxWeight, double minWeight, double maxDiameter, double minDiameter, double value)
        {
            MaxWeight = maxWeight;
            MinWeight = minWeight;
            MaxDiameter = maxDiameter;
            MinDiameter = minDiameter;
            Value = value;
        }

        public double MaxWeight { get; }
        public double MinWeight { get; }
        public double MaxDiameter { get; }
        public double MinDiameter { get; }

        public double Value { get; }

        public override string ToString()
        {
            return $"Coin Value {Value}";
        }
    }
}