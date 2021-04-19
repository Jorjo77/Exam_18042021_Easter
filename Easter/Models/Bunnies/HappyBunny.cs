namespace Easter.Models.Bunnies
{
    public class HappyBunny : Bunny
    {
        private const int Energy = 100;
        public HappyBunny(string name) 
            : base(name, Energy)
        {
        }
    }
}
