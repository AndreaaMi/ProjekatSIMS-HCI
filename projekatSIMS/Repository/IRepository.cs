using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(Entity entity);
        
        void Remove(Entity entity);

        void Edit(Entity entity);

        Entity Get(int id);

        IEnumerable<Entity> GetAll();

        IEnumerable<Entity> Search(string term = "");

        int GenerateId();

        void Save();
    }
}
