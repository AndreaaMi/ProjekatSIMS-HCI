using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    class AccommodationService
    {
        public void Add(Accommodation accommodation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Accommodation.Add(accommodation);
            unitOfWork.Save();
        }

        public void Edit(Accommodation accommodation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Accommodation.Edit(accommodation);
            unitOfWork.Save();
        }

        public void Remove(Accommodation accommodation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Accommodation.Remove(accommodation);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Accommodation.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Accommodation.GetAll();
        }

       // public IEnumerable<Entity> Search(string term = "")
       // {
         //   UnitOfWork unitOfWork = new UnitOfWork();
        //    return unitOfWork.Accommodation.Search(term);
      //  }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Accommodation.GenerateId();
        }
    }
}
