using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class TourRequestRepository : Repository<TourRequest>
    {
        public override void Edit(Entity entity)
        {
            Entity tourRating = base.Get(entity.Id);
            ((TourRequest)tourRating).Location.Country = ((TourRequest)entity).Location.Country;
            ((TourRequest)tourRating).Location.City = ((TourRequest)entity).Location.City;
            ((TourRequest)tourRating).Description = ((TourRequest)entity).Description;
            ((TourRequest)tourRating).Language = ((TourRequest)entity).Language;
            ((TourRequest)tourRating).GuestNumber = ((TourRequest)entity).GuestNumber;
            ((TourRequest)tourRating).StartDate = ((TourRequest)entity).StartDate;
            ((TourRequest)tourRating).EndDate = ((TourRequest)entity).EndDate;
            ((TourRequest)tourRating).Status = ((TourRequest)entity).Status;

        }
    }
}
