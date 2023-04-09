using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class ReservationRescheduleRequestService
    {
        public void Add(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.ReservationRescheduleRequests.Add(reservationRescheduleRequest);
            unitOfWork.Save();
        }

        public void Edit(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.ReservationRescheduleRequests.Edit(reservationRescheduleRequest);
            unitOfWork.Save();
        }

        public void Remove(ReservationRescheduleRequest reservationRescheduleRequest)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.ReservationRescheduleRequests.Remove(reservationRescheduleRequest);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.ReservationRescheduleRequests.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.ReservationRescheduleRequests.GetAll();
        }

        public IEnumerable<Entity> Search(string term = "")
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.ReservationRescheduleRequests.Search(term);
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.ReservationRescheduleRequests.GenerateId();
        }
    }
}
