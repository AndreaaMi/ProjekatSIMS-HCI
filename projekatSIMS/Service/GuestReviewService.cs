using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekatSIMS.Service
{
    internal class GuestReviewService
    {
        public void Add(GuestReview guestReview)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.GuestReviews.Add(guestReview);
            unitOfWork.Save();
        }

        public void Edit(GuestReview guestReview)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.GuestReviews.Edit(guestReview);
            unitOfWork.Save();
        }

        public void Remove(GuestReview guestReview)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.GuestReviews.Remove(guestReview);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.GuestReviews.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.GuestReviews.GetAll();
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.GuestReviews.GenerateId();
        }

        public bool CheckDate(DateTime date)
        {
            DateTime currentDate = DateTime.Now;

            if (currentDate > date)
            {
                return false;
            }

            return true;
        }


        /* public void SetTimer(DateTime endDate)
         {
             var timer = new System.Timers.Timer();
             timer.Elapsed += (sender, e) => {
                 if (DateTime.Now > endDate)
                 {
                     timer.Stop();
                 }
                 else
                 {
                     MessageBox.Show("Molimo vas da ocenite gosta.");
                 }
             };
             timer.Interval = TimeSpan.FromDays(1).TotalMilliseconds;
             timer.Start();
         }*/

        public bool CheckGrades(int cleanliness, int respectingRules)
        {
            if (cleanliness < 1 || cleanliness > 5 || respectingRules < 1 || respectingRules > 5)
            {
                return false;
            }
            return true;
        }

       



        public void PlaceGuestReview(GuestReview guestReview)
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            DateTime endDate = new DateTime();
            endDate = guestReview.accommodationReservation.EndDate.AddDays(5);

            if (CheckDate(endDate) == false)
            {
                MessageBox.Show("Vaš rok za ocenjivanje gosta je istekao.");
                return;
            }

            if (CheckDate(guestReview.AccommodationReservation.EndDate) != false)
            {
                MessageBox.Show("Nemate pravo da ocenite gosta dok se rezervacija ne zavrsi.");
                return;
            }


            else if (unitOfWork.GuestReviews.GetGuestReviewByAccommodation(guestReview.accommodationReservation.Id) != null)
            {

                MessageBox.Show("Vec ste ocenili ovog gosta!");
                return;

            }
            else if (!CheckGrades(guestReview.Cleanliness, guestReview.RespectingRules))
            {
                MessageBox.Show("Mozete da unesete ocenu samo od 1 do 5.");
                return;
            }

            else
            {
                guestReview.Id = GenerateId();
                Add(guestReview);
            }

        }


    }
}
