using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekatSIMS.Service
{
    class AccommodationService
    {
        public void Add(Accommodation accommodation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Accommodations.Add(accommodation);
            unitOfWork.Save();
        }

        public void Edit(Accommodation accommodation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Accommodations.Edit(accommodation);
            unitOfWork.Save();
        }

        public void Remove(Accommodation accommodation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Accommodations.Remove(accommodation);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Accommodations.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Accommodations.GetAll();
        }

       public IEnumerable<Entity> Search(string term = "")
        {
          UnitOfWork unitOfWork = new UnitOfWork();
           return unitOfWork.Accommodations.Search(term);
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Accommodations.GenerateId();
        }

        public Accommodation GetAccommodationByName(string name)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Accommodation result = null;
            foreach (Accommodation it in unitOfWork.Accommodations.GetAll())
            {
                if (it.Name == name)
                {
                    result = it;
                }
            }
            return result;
        }

        public void RegisterAccommodation(Accommodation accommodation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            if (unitOfWork.Accommodations.GetAccommodationByNameCityAndCountry(accommodation) != null)
            {
                MessageBox.Show("Smestaj sa unetim podacima vec postoji.");
                return;

            }
            else
            {

                // registrovanje novog smeštaja
                int id = GenerateId();
                accommodation.Id = id;
                unitOfWork.Accommodations.Add(accommodation);
                unitOfWork.Save();
            }
        }
    }
}
