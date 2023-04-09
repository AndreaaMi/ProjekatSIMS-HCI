using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class AccommodationRepository : Repository<Accommodation>
    {
        public override void Edit(Entity entity)
        {
            Entity accommodation = base.Get(entity.Id);

            ((Accommodation)accommodation).OwnerId = ((Accommodation)entity).OwnerId;
            ((Accommodation)accommodation).Id = ((Accommodation)entity).Id;
            ((Accommodation)accommodation).Name = ((Accommodation)entity).Name;
            ((Accommodation)accommodation).Location = ((Accommodation)entity).Location;
            ((Accommodation)accommodation).Type = ((Accommodation)entity).Type;
            ((Accommodation)accommodation).GuestLimit = ((Accommodation)entity).GuestLimit;
            ((Accommodation)accommodation).MinimumStayDays = ((Accommodation)entity).MinimumStayDays;
            ((Accommodation)accommodation).CancellationDays = ((Accommodation)entity).CancellationDays;
            ((Accommodation)accommodation).ImageUrls = ((Accommodation)entity).ImageUrls;
        }

        public override IEnumerable<Entity> Search(string term = "")
        {
            List<Entity> result = new List<Entity>();
            foreach (Entity it in DataContext.Instance.Accommodations)
            {
                if (((Accommodation)it).Name.Contains(term))
                {
                    result.Add(it);
                }
            }

            return result;
        }


        public Accommodation GetAccommodationByType(AccommodationType type)
        {
            foreach (Accommodation it in DataContext.Instance.Accommodations)
            {
                if (it.Type == type)
                {
                    return it;
                }
            }

            return null;
        }

        public Accommodation GetAccommodationById(int id)
        {
            foreach (Accommodation it in DataContext.Instance.Accommodations)
            {
                if (it.Id == id)
                {
                    return it;
                }
            }

            return null;
        }

        public Accommodation GetAccommodationByName(string name)
        {
            foreach (Accommodation it in DataContext.Instance.Accommodations)
            {
                if (it.Name == name)
                {
                    return it;
                }
            }

            return null;
        }

        public Accommodation GetAccommodationByLocation(Location location)
        {
            foreach (Accommodation it in DataContext.Instance.Accommodations)
            {
                if (it.Location == location)
                {
                    return it;
                }
            }

            return null;
        }


        public Accommodation GetAccommodationByNameCityAndCountry(Accommodation accommodation)
        {
            foreach (Accommodation it in DataContext.Instance.Accommodations)
            {
                if (it.Name == accommodation.Name & it.Location.City == accommodation.Location.City & it.Location.Country == accommodation.Location.country)
                {
                    return it;
                }
            }

            return null;
        }

    }
}