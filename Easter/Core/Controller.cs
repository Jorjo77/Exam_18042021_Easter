using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Core
{
    public class Controller : IController
    {

        private List<IBunny> bunnyRepository;
        private List<IEgg> eggRepository;
        private int countColoredEggs = 0;

        public Controller()
        {
            bunnyRepository = new List<IBunny>();
            eggRepository = new List<IEgg>();
        }
        public ICollection<IBunny> BunnyRepository
            => bunnyRepository.AsReadOnly();
        public ICollection<IEgg> EggRepository
            => eggRepository.AsReadOnly();
        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny bunny = null;
            string message = string.Empty;
            switch (bunnyType)
            {
                case "HappyBunny":
                    bunny = new HappyBunny(bunnyName);
                    bunnyRepository.Add(bunny);
                    message = string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
                    break;
                case "SleepyBunny":
                    bunny = new SleepyBunny(bunnyName);
                    bunnyRepository.Add(bunny);
                    message = string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
                    break;
                default:
                    message = ExceptionMessages.InvalidBunnyType;
                    break;
            }
            return message;
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            string message = string.Empty;
            var searchedBunny = bunnyRepository.FirstOrDefault(b => b.Name == bunnyName);
            if (searchedBunny == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }
            else
            {
                var dye = new Dye(power);
                searchedBunny.AddDye(dye);
                message = string.Format(OutputMessages.DyeAdded, power, bunnyName);
            }
            return message;
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            string message = string.Empty;
            var egg = new Egg(eggName, energyRequired);
            eggRepository.Add(egg);
            message = string.Format(OutputMessages.EggAdded, eggName);
            return message;
        }

        public string ColorEgg(string eggName)
        {

            string message = string.Empty;
            var givenEgg = eggRepository.FirstOrDefault(e => e.Name == eggName);
            var theMostReadyBunny = bunnyRepository.OrderByDescending(b => b.Energy).First();
            theMostReadyBunny.Work();
            givenEgg.GetColored();
            if (theMostReadyBunny.Energy < 50)
            {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            }
            if (theMostReadyBunny.Energy <=0)
            {
                bunnyRepository.Remove(theMostReadyBunny);
                message = string.Format(OutputMessages.EggIsNotDone, eggName);
            }
            else
            {
                message = string.Format(OutputMessages.EggIsDone, eggName);

                countColoredEggs++;
            }
            return message;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            string message = string.Empty;

            foreach (var bunny in bunnyRepository)
            {
                sb.AppendLine($"Name: {bunny.Name}");
                sb.AppendLine($"Energy: {bunny.Energy}");
                sb.AppendLine($"Dyes: {bunny.Dyes.Count} not finished");
            }

            message = $"{countColoredEggs} eggs are done!" + Environment.NewLine +
            "Bunnies info:" + Environment.NewLine + sb.ToString().TrimEnd();
            return message;
        }
    }
}
