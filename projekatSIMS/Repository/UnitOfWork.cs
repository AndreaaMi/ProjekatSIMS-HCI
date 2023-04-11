using projekatSIMS.Model;
using projekatSIMS.Service;
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
            Tours = new TourRepository();
            Accommodations = new AccommodationRepository();
            KeyPoint = new KeyPointsRepository();
            TourReservations = new TourReservationRepository();
            AccommodationReservations = new AccommodationReservationRepository();
            GuestReviews = new GuestReviewRepository();
            ReservationRescheduleRequests = new ReservationRescheduleRequestRepository();
            AccommodationOwnerRatings = new AccommodationOwnerRatingRepository();
            Vouchers = new VoucherRepository();
            TourRatings = new TourRatingRepository();
        }

        public UserRepository Users { get; private set; }
        public AccommodationRepository Accommodations { get; private set; }
        public TourRepository Tours { get; private set; }
        public KeyPointsRepository KeyPoint { get; private set; }
        public TourReservationRepository TourReservations { get; private set; }
        
        public AccommodationReservationRepository AccommodationReservations { get; private set; }
        
        public GuestReviewRepository GuestReviews { get; private set; }

        public ReservationRescheduleRequestRepository ReservationRescheduleRequests { get; private set; } 

        public AccommodationOwnerRatingRepository AccommodationOwnerRatings { get; private set; }

        public VoucherRepository Vouchers { get; private set; }

        public TourRatingRepository TourRatings { get; private set; }
        public void Save()
        {
            DataContext.Instance.Save(); //Save je stavljen da bi se mogli sacuvati podaci nakon metoda iz servisa
        }
        

           
    }

        

        
        

       


    }

