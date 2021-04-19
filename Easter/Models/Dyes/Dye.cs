using Easter.Models.Dyes.Contracts;

namespace Easter.Models.Dyes
{
    public class Dye : IDye
    {
        private int power;

        public Dye(int power)
        {
            Power = power;
        }

        public int Power
        {
            get 
            {
                return power; 
            }
            private set 
            {
                if (value < 0)
                {
                    power = 0;
                }
                power = value; 
            }
        }


        public bool IsFinished()
            => Power == 0;

        public void Use()
        {
            this.Power -= 10;
            if (this.Power < 0)
            {
                this.Power = 0;
                this.IsFinished();
            }
        }
    }
}
