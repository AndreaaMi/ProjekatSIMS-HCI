using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class AccommodationRenovationRecommendationService
    {
        public void Add(AccommodationRenovationRecommendation accommodationRenovationRecommendation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.AccommodationRenovationRecommendations.Add(accommodationRenovationRecommendation);
            unitOfWork.Save();
        }

        public void Edit(AccommodationRenovationRecommendation accommodationRenovationRecommendation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.AccommodationRenovationRecommendations.Edit(accommodationRenovationRecommendation);
            unitOfWork.Save();
        }

        public void Remove(AccommodationRenovationRecommendation accommodationRenovationRecommendation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.AccommodationRenovationRecommendations.Remove(accommodationRenovationRecommendation);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationRenovationRecommendations.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationRenovationRecommendations.GetAll();
        }

        public IEnumerable<Entity> Search(string term = "")
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationRenovationRecommendations.Search(term);
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationRenovationRecommendations.GenerateId();
        }

    }
}
