using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class AccommodationReservationRepository : Repository<AccommodationReservation>
    {
        public override void Edit(Entity entity)
        {
            Entity accommodationReservation = base.Get(entity.Id);

            ((AccommodationReservation)accommodationReservation).Id = ((AccommodationReservation)entity).Id;
            ((AccommodationReservation)accommodationReservation).AccommodationName = ((AccommodationReservation)entity).AccommodationName;
            ((AccommodationReservation)accommodationReservation).StartDate = ((AccommodationReservation)entity).StartDate;
            ((AccommodationReservation)accommodationReservation).EndDate = ((AccommodationReservation)entity).EndDate;
            ((AccommodationReservation)accommodationReservation).GuestCount = ((AccommodationReservation)entity).GuestCount;
            ((AccommodationReservation)accommodationReservation).GuestsRate = ((AccommodationReservation)entity).GuestsRate;
            ((AccommodationReservation)accommodationReservation).OwnersRate = ((AccommodationReservation)entity ).OwnersRate;

        }

        public override IEnumerable<Entity> Search(string term = "")
        {
            List<Entity> result = new List<Entity>();
            foreach (Entity it in DataContext.Instance.AccommodationReservations)
            {
                if (((AccommodationReservation)it).AccommodationName.Contains(term))
                {
                    result.Add(it);
                }
            }

            return result;
        }
        public AccommodationReservation GetAccommodationReservationById(int id)
        {
            foreach (AccommodationReservation it in DataContext.Instance.AccommodationReservations)
            {
                if (it.Id == id)
                {
                    return it;
                }
            }
            return null;
        }

        public IEnumerable<Entity> GetAccommodationReservationByAccommodation(string accommodationName)
        {
            List<Entity> result = new List<Entity>();
            foreach (AccommodationReservation it in DataContext.Instance.AccommodationReservations)
            {
                if (it.AccommodationName == accommodationName)
                {
                    result.Add(it);
                }
            }
            return null;
        }

    }
}
