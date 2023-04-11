using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class TourRepository : Repository<Tour>
    {
        public override void Edit(Entity entity)
        {
            Entity tour = base.Get(entity.Id);

            ((Tour)tour).Id = ((Tour)entity).Id;
            ((Tour)tour).Name = ((Tour)entity).Name;
            ((Tour)tour).Location.Country = ((Tour)entity).Location.Country;
            ((Tour)tour).Location.City = ((Tour)entity).Location.City;
            ((Tour)tour).Language = ((Tour)entity).Language;
            ((Tour)tour).StartingTime = ((Tour)entity).StartingTime;
            ((Tour)tour).MaxNumberOfGuests = ((Tour)entity).MaxNumberOfGuests;
            ((Tour)tour).Duration = ((Tour)entity).Duration;
            ((Tour)tour).KeyPoints = ((Tour)entity).KeyPoints;
        }

        public int[] GetTourKeypoints(int tourId)
        {
            foreach(Tour tour in DataContext.Instance.Tours)
            {
                if (tour.Id == tourId)
                {
                    int[] keypointIds = new int[tour.KeyPoints.Count];
                    for (int i = 0; i < tour.KeyPoints.Count; i++)
                    {
                        keypointIds[i] = tour.KeyPoints[i].Id;
                    }
                    return keypointIds;
                }
            }
            return new int[0];
        }




    }
}
