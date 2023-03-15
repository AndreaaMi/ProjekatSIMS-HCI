using projekatSIMS.Model;
<<<<<<< HEAD
=======
using projekatSIMS.Service;
>>>>>>> findAllTours
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class UnitOfWork
    {
        public UnitOfWork() //Klasa koja ce sluziti za komunikaciju sa servisima - POSEDUJE SVAKI REPOSITORY 
        {
            Users = new UserRepository();
            Accommodations = new AccommodationRepository();
        }

        public UserRepository Users { get; private set; }

        public AccommodationRepository Accommodations { get; private set; }

        public void Save()
        {
            SIMSContext.Instance.Save(); //Save je stavljen da bi se mogli sacuvati podaci nakon metoda iz servisa
        }


=======
            Tours = new TourRepository();
        }

        public UserRepository Users { get; private set; }
        public TourRepository Tours { get; private set; }

        public void Save() 
        {
            SIMSContext.Instance.Save(); //Save da bi se mogli sacuvati podaci nakon metoda iz servisa
        }
>>>>>>> findAllTours
    }
}
