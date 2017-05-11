namespace Vending.Domain
{
    public class Token
    {
        public Token(decimal weight, decimal diameter)
        {
            Weight = weight;
            Diameter = diameter;
        }


        public decimal Weight { get; set; }
        public decimal Diameter { get; set; }

        public override string ToString()
        {
            return $"Token - Weight {Weight} Diameter {Diameter}";
        }
    }
}