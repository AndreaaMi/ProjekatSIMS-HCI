using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class AccommodationReservationService
    {
        public void Add(AccommodationReservation accommodationReservation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.AccommodationReservations.Add(accommodationReservation);
            unitOfWork.Save();
        }

        public void Edit(AccommodationReservation accommodationReservation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.AccommodationReservations.Edit(accommodationReservation);
            unitOfWork.Save();
        }

        public void Remove(AccommodationReservation accommodationReservation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.AccommodationReservations.Remove(accommodationReservation);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationReservations.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationReservations.GetAll();
        }

        public IEnumerable<Entity> Search(string term = "")
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationReservations.Search(term);
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationReservations.GenerateId();
        }


        public void CreateAccommodationReservation(AccommodationReservation accommodationReservation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            if (unitOfWork.AccommodationReservations.GetAccommodationReservationById(accommodationReservation.Id) != null)
            {
                throw new Exception("ID already exist!");
            }
            unitOfWork.AccommodationReservations.Add(accommodationReservation);
            unitOfWork.Save();
        }
    }
}
