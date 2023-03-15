using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class UserService
    {
        //Implementira osnovne metode i moze imati posebne metode u zavisnosti od funkcionalnosti
        public void Add(User user)
        {
            UnitOfWork unitOfWork = new UnitOfWork(); //Instanciramo UnitOfWork jer cemo preko njega pristupiti repositorijumu
            unitOfWork.Users.Add(user);//Pozovemo odgovarajuci repo i odogvarajucu funkciju
            unitOfWork.Save(); //Sacuvas podatke
        }

        public void Edit(User user)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Users.Edit(user);
            unitOfWork.Save();
        }

        public void Remove(User user)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Users.Remove(user);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Users.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Users.GetAll();
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Users.GenerateId();
        }

        public IEnumerable<Entity> Search(string term = "")
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Users.Search(term);
        }

        public bool CheckLogin(string user, string pass)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            foreach (User it in unitOfWork.Users.GetAll())
            {
                if (it.FirstName.Equals(user) && it.Password.Equals(pass))
                {
                    unitOfWork.Users.SetLoginUser(it);
                    return true;
                }
            }
            return false;
        }

        public User GetLoginUser()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Users.GetLoginUser();
        }

        public void GetLoginUser(User user)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Users.SetLoginUser(user);
        }


    }
}
