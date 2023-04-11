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

        public int GetRequestOwnerId(ReservationRescheduleRequest request)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            AccommodationReservation reservation = (AccommodationReservation)unitOfWork.AccommodationReservations.Get(request.ReservationId);
            Accommodation accommodation = (Accommodation)unitOfWork.Accommodations.GetAccommodationByName(reservation.AccommodationName);
            int ownerId = accommodation.OwnerId;
            return ownerId;
        }

        public bool AreNewRequestDatesAvailable(ReservationRescheduleRequest request)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Entity> existingReservations = (List<Entity>)unitOfWork.AccommodationReservations.GetAll(); // Preuzmi sve postojeće rezervacije

            foreach (AccommodationReservation reservation in existingReservations)
            {
                if (reservation.Id == request.ReservationId &&
                    !(reservation.StartDate <= request.NewStartDate || reservation.StartDate >= request.NewEndDate))
                {
                    return false;

                }
            }
            return true;

        }

        public void AcceptRequest(ReservationRescheduleRequest request)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            AccommodationReservation reservation = (AccommodationReservation)unitOfWork.AccommodationReservations.GetAccommodationReservationById(request.ReservationId);
            reservation.StartDate = request.NewStartDate;
            reservation.EndDate = request.NewEndDate;

            unitOfWork.AccommodationReservations.Edit(reservation);
            request.Status = RequestStatusType.APPROVED;
            request.Comment = "";
            unitOfWork.ReservationRescheduleRequests.Edit(request);
            unitOfWork.Save();
        }

        public void RejectRequest(ReservationRescheduleRequest request, string comment = "")
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            request.Status = RequestStatusType.REJECTED;
            request.Comment = comment;
            unitOfWork.ReservationRescheduleRequests.Edit(request);
            unitOfWork.Save();
        }

    }
}
