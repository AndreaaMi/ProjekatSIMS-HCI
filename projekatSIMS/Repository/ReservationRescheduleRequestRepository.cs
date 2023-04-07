using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class ReservationRescheduleRequestRepository : Repository<ReservationRescheduleRequest>
    {
        public override void Edit(Entity entity)
        {
            Entity reservationRescheduleRequest = base.Get(entity.Id);
            ((ReservationRescheduleRequest)reservationRescheduleRequest).Id = ((ReservationRescheduleRequest)entity).Id;
            ((ReservationRescheduleRequest)reservationRescheduleRequest).ReservationId = ((ReservationRescheduleRequest)entity).ReservationId;
            ((ReservationRescheduleRequest)reservationRescheduleRequest).NewStartDate = ((ReservationRescheduleRequest)entity).NewStartDate;
            ((ReservationRescheduleRequest)reservationRescheduleRequest).NewEndDate = ((ReservationRescheduleRequest)entity).NewEndDate;
            ((ReservationRescheduleRequest)reservationRescheduleRequest).GuestName = ((ReservationRescheduleRequest)entity).GuestName;
            ((ReservationRescheduleRequest)reservationRescheduleRequest).Status = ((ReservationRescheduleRequest)entity).Status;

        }

        public override IEnumerable<Entity> Search(string term = "")
        {
            return base.Search(term);
        }
    }
}
