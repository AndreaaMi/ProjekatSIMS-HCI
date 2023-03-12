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
        }

        public UserRepository Users { get; private set; }
    }
}
