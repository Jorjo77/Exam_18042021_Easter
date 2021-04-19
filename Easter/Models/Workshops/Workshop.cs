using Easter.Models.Bunnies.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using System.Linq;

namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public void Color(IEgg egg, IBunny bunny)
        {

            if (bunny.Energy > 0 && bunny.Dyes.Any(d => d.Power > 0))
            {

                while (!egg.IsDone())
                {
                    var usedDye = bunny.Dyes.FirstOrDefault(d => d.Power > 0);
                    if (usedDye!=null)
                    {
                        usedDye.Use();
                        egg.GetColored();
                    }
                    else
                    {
                        var HasNewDay = bunny.Dyes.Any(d => d.Power > 0);
                        if (HasNewDay)
                        {
                            var newDay = bunny.Dyes.First(d => d.Power > 0);
                            newDay.Use();
                            egg.GetColored();
                        }
                        else
                        {
                            bunny.Dyes.Remove(usedDye);
                            break;
                        }

                    }

                    if (bunny.Energy<0|| bunny.Dyes.All(d => d.Power == 0))
                    {
                        break;
                    }
                }
            }
        }
    }
}
