namespace Vending
{
    public class Token
    {
        public Token(double weight, double diameter)
        {
            Weight = weight;
            Diameter = diameter;
        }


        public double Weight { get; set; }
        public double Diameter { get; set; }

        public override string ToString()
        {
            return $"Token - Weight {Weight} Diameter {Diameter}";
        }
    }
}