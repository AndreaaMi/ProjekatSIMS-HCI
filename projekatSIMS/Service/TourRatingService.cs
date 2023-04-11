using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class TourRatingService
    {
        public void Add(TourRating tourRating)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.TourRatings.Add(tourRating);
            unitOfWork.Save();
        }

        public void Edit(TourRating tourRating)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.TourRatings.Edit(tourRating);
            unitOfWork.Save();
        }

        public void Remove(TourRating tourRating)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.TourRatings.Remove(tourRating);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.TourRatings.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            IEnumerable<Entity> tourRatings = unitOfWork.TourRatings.GetAll();
            return tourRatings;
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.TourRatings.GenerateId();
        }
    }
}
