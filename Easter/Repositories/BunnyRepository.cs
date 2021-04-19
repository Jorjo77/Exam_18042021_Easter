
using Easter.Models.Bunnies.Contracts;
using Easter.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Easter.Repositories
{
    public class BunnyRepository : IRepository<IBunny>
    {
        private List<IBunny> models;

        public BunnyRepository()
        {
            models = new List<IBunny>();
        }
        public IReadOnlyCollection<IBunny> Models 
            => models.AsReadOnly();

        public void Add(IBunny model)
        {
            models.Add(model);
        }

        public IBunny FindByName(string name)
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

        public bool Remove(IBunny model)
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
