using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;

namespace Easter.Models.Bunnies
{
    public abstract class Bunny : IBunny
    {
        private string name;
        private int energy;
        private List<IDye> dyes = new List<IDye>();

        protected Bunny(string name, int energy)
        {
            Name = name;
            Energy = energy;
        }

        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBunnyName);
                }
                name = value;
            }
        }

        public int Energy
        {
            get
            {
                return energy;
            }
            protected set
            {
                if (value < 0)
                {
                    energy = 0;
                }
                energy = value;
            }
        }

        public ICollection<IDye> Dyes
            => dyes.AsReadOnly();

        public void AddDye(IDye dye)
        {
            dyes.Add(dye);
        }

        public virtual void Work()
        {
            this.Energy -= 10;
            if (this.Energy < 0)
            {
                this.Energy = 0;
            }
        }
    }
}
