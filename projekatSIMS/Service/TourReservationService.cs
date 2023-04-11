using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class TourReservationService
    {
        public void Add(TourReservation tourReservation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.TourReservations.Add(tourReservation);
            unitOfWork.Save();
        }

        public void Edit(TourReservation tourReservation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.TourReservations.Edit(tourReservation);
            unitOfWork.Save();
        }

        public void Remove(TourReservation tourReservation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.TourReservations.Remove(tourReservation);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.TourReservations.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork= new UnitOfWork();
            IEnumerable<Entity> tourReservations = unitOfWork.TourReservations.GetAll();
            return tourReservations;
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.TourReservations.GenerateId();
        }

        public void TourReservating(TourReservation tourReservation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            if(unitOfWork.TourReservations.Get(tourReservation.Id) != null)
            {
                throw new Exception("ID already exists!");
            }
            unitOfWork.TourReservations.Add(tourReservation);
            unitOfWork.Save();
        }

        public List<int> GetReservationByGuestId(int guestId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.TourReservations.GetReservationByGuestId(guestId);
        }
    }
}
