using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class TourService
    {
        public void Add(Tour tour) 
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Tours.Add(tour);
            unitOfWork.Save();
        }

        public void Edit(Tour tour)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Tours.Edit(tour);
            unitOfWork.Save();
        }

        public void Remove(Tour tour)
        {   UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Tours.Remove(tour);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Tours.Get(id);
        }

        public IEnumerable<Entity> GetAll() 
        {
            UnitOfWork unitOfWork= new UnitOfWork();
            IEnumerable<Entity> tours = unitOfWork.Tours.GetAll();
            return tours;
        }

        public int[] GetTourKeypoints(int tourId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Tours.GetTourKeypoints(tourId);
        }
    }
}
