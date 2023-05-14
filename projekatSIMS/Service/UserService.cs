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

        public void CheckCredentials(string email, string pass)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            foreach (User user in unitOfWork.Users.GetAll())
            {
                if (user.Email.Equals(email) && user.Password.Equals(pass))
                {
                    SetLoginUser(user);
                }
            }
        }
        public int GetCurrentUserId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            User loginUser = unitOfWork.Users.GetLoginUser();
            return loginUser.Id;
        }
        public string GetLoginUserFirstName()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            User loginUser = unitOfWork.Users.GetLoginUser();
            return loginUser.FirstName;
        }

        public User GetLoginUser()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Users.GetLoginUser();
        }

        public void SetLoginUser(User user)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Users.SetLoginUser(user);
        }

        public string GetLoginUserType()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Users.GetLoginUserType();
        }

        public void UpdateOwnerRating(int ownerId, double ownerPolitness,double cleanliness)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            User owner = (User)unitOfWork.Users.Get(ownerId);

            if (owner == null)
            {
                throw new ArgumentException("Vlasnik sa datim ID-jem nije pronađen.");
            }


            double currentRating = owner.AverageRating;
            int reviewCount = owner.ReviewCount;

            double newAverageRating = ((currentRating * reviewCount) + ownerPolitness + cleanliness) / (reviewCount + 1);
            owner.AverageRating = newAverageRating;
            owner.ReviewCount = reviewCount + 1;

            // Save changes to the repository
            unitOfWork.Users.Edit(owner);
            unitOfWork.Save();
        }

        public bool IsSuperOwner(int ownerId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            User owner = (User)unitOfWork.Users.Get(ownerId);
            return (owner.ReviewCount >= 50) && (owner.AverageRating >= 9.5);
        }

        public void SetSuperOwner(int ownerId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            User owner = (User)unitOfWork.Users.Get(ownerId);

            if (owner != null && IsSuperOwner(ownerId))
            {
                owner.SuperStatus = true;
            }

            owner.SuperStatus = false;
            unitOfWork.Users.Edit(owner);
            unitOfWork.Save();

        }
        public void UpdateReservationCount(int userId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            User user = (User)unitOfWork.Users.Get(userId);
            if (user != null)
            {
                user.ReservationCount++;
                unitOfWork.Users.Edit(user); // Ažurirajte korisnika sa novim brojem rezervacija
                unitOfWork.Save(); // Sačuvajte promene u bazi podataka

                if (user.ReservationCount >= 10 && !user.IsSuperGuest)
                {
                    user.IsSuperGuest = true;
                    user.BonusPoints = 5;
                    user.ReservationCount = 0;
                    user.SuperGuestExpirationDate = DateTime.Now.AddYears(1);
                    unitOfWork.Users.Edit(user); // Ažurirajte korisnika sa statusom super-gosta
                    unitOfWork.Save(); // Sačuvajte promene u bazi podataka
                }
                else
                {
                    if(user.IsSuperGuest && user.ReservationCount < 10)
                    {
                        unitOfWork.Users.Edit(user); // Ažurirajte korisnika sa statusom super-gosta
                        unitOfWork.Save();
                    }
                }
            }
        }
        public void UpdateSuperGuestStatus(int userId, int reservationCount)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            User user = (User)unitOfWork.Users.Get(userId);

            if (user == null)
            {
                throw new ArgumentException("Korisnik sa datim ID-jem nije pronađen.");
            }

            if (user.IsSuperGuest)
            {
                if (reservationCount < 10 && DateTime.Now > user.SuperGuestExpirationDate)
                {
                    user.IsSuperGuest = false;
                    user.BonusPoints = 0;
                }
            }
            else
            {
                if (reservationCount >= 10)
                {
                    user.IsSuperGuest = true;
                    user.ReservationCount = 0;
                    user.BonusPoints = 5;
                    user.SuperGuestExpirationDate = DateTime.Now.AddYears(1);
                }
            }

            unitOfWork.Users.Edit(user);
            unitOfWork.Save();
        }

        public bool UseBonusPoint(int userId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            User user = (User)unitOfWork.Users.Get(userId);

            if (user == null)
            {
                throw new ArgumentException("Korisnik sa datim ID-jem nije pronađen.");
            }

            if (user.IsSuperGuest && user.BonusPoints > 0)
            {
                user.BonusPoints--;
                unitOfWork.Users.Edit(user);
                unitOfWork.Save();
                return true;
            }

            return false;
        }

    }
}
