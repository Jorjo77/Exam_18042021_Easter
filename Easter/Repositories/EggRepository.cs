using Easter.Models.Eggs.Contracts;
using Easter.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Easter.Repositories
{
    public class EggRepository : IRepository<IEgg>
    {
        private List<IEgg> models;

        public EggRepository()
        {
            models = new List<IEgg>();
        }
        public IReadOnlyCollection<IEgg> Models 
            => models.AsReadOnly();

        public void Add(IEgg model)
        {
            models.Add(model);
        }

        public IEgg FindByName(string name)
        {
            var searchedModel = models.FirstOrDefault(m => m.Name == name);
            if (searchedModel == null)
            {
                return null;
            }
            else
            {
                return searchedModel;
            }
        }

        public bool Remove(IEgg model)
        {
            if (models.Contains(model))
            {
                models.Remove(model);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
