using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class //Genericki repozitorijum
    {
        public void Add(Entity entity)
        {
            DataContext.Instance.GetAllEntitiesOfType(typeof(TEntity)).Add(entity);
        }

        public virtual void Edit(Entity entity) {
        }

        public int GenerateId()
        {
            return DataContext.Instance.GenerateId(DataContext.Instance.GetAllEntitiesOfType(typeof(TEntity)));
        }

        public Entity Get(int id)
        {
            return DataContext.Instance.GetAllEntitiesOfType(typeof(TEntity)).Where(x => x.Id == id).FirstOrDefault(); //OVO u zagradi se cita daj mi x tako da je njegov id = prosledjenom
        }

        public IEnumerable<Entity> GetAll()
        {
            return DataContext.Instance.GetAllEntitiesOfType(typeof(TEntity));
        }
        public virtual IEnumerable<Entity> Search(string term = "")
        {
            throw new NotImplementedException();  //Da nema ovoga ne bi moglo bacao bi gresku jer nema return vrednosti
        }
        public void Remove(Entity entity)
        {
            DataContext.Instance.GetAllEntitiesOfType(typeof(TEntity)).Remove(entity);
        }

        public void Save()
        {
            DataContext.Instance.Save(); //Ode u bazu , pozove save i dalje se izvrsi svaka generic save funkcija
        }
    }
}
