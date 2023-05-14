using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel
{
    public class DisplayReviewsViewModel : ViewModelBase
    {
        private ObservableCollection<AccommodationReservation> reservationItems = new ObservableCollection<AccommodationReservation>();
        private AccommodationReservation selectedReservation;
        private GuestReviewService guestReviewService;
        private AccommodationOwnerRatingService accommodationOwnerRatingService;
        private AccommodationReservationService accommodationReservationService;
        private string comment;
        private int cleanlinessRating1;
        private int ownerPolitenessRating;
        private int cleanlinessRating2;
        private int respectingRulesRating;
        private string comment2;

        public bool IsReservationSelected => selectedReservation != null;

        public AccommodationReservation SelectedReservation
        {
            get { return selectedReservation; }
            set
            {
                if (selectedReservation != value)
                {
                    selectedReservation = value;
                    OnPropertyChanged(nameof(SelectedReservation));
                    OnPropertyChanged(nameof(IsReservationSelected));
                }
                if (selectedReservation != null)
                {
                    LoadSelectedReservationData();
                }
            }

        }

        public int RespectingRulesRating
        {
            get { return respectingRulesRating; }
            set
            {
                if (respectingRulesRating != value)
                {
                    respectingRulesRating = value;
                    OnPropertyChanged(nameof(RespectingRulesRating));
                }
            }
        }
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }
        public string Comment2
        {
            get { return comment2; }
            set
            {
                if (comment2 != value)
                {
                    comment2 = value;
                    OnPropertyChanged(nameof(Comment2));
                }
            }
        }

        public int CleanlinessRating1
        {
            get { return cleanlinessRating1; }
            set
            {
                if (cleanlinessRating1 != value)
                {
                    cleanlinessRating1 = value;
                    OnPropertyChanged(nameof(CleanlinessRating1));
                }
            }
        }

        public int CleanlinessRating2
        {
            get { return cleanlinessRating2; }
            set
            {
                if (cleanlinessRating2 != value)
                {
                    cleanlinessRating2 = value;
                    OnPropertyChanged(nameof(CleanlinessRating2));
                }
            }
        }

        public int OwnerPolitenessRating
        {
            get { return ownerPolitenessRating; }
            set
            {
                if (ownerPolitenessRating != value)
                {
                    ownerPolitenessRating = value;
                    OnPropertyChanged(nameof(OwnerPolitenessRating));
                }
            }
        }


        public DisplayReviewsViewModel()
        {
            guestReviewService = new GuestReviewService();
            accommodationOwnerRatingService = new AccommodationOwnerRatingService();
            accommodationReservationService = new AccommodationReservationService();    
            LoadData();

        }

        public void LoadSelectedReservationData()
        {
            if (SelectedReservation != null)
            {
                AccommodationOwnerRating ownerRating = GetAccommodationOwnerRatingByReservationId(SelectedReservation.Id);
                GuestReview guestReview = GetGuestReviewByReservationId(SelectedReservation.Id);

                if (ownerRating != null)
                {
                    Comment = ownerRating.Comment;
                    CleanlinessRating1 = ownerRating.Cleanliness;
                    OwnerPolitenessRating = ownerRating.OwnerPoliteness;
                }

                if (guestReview != null)
                {
                    CleanlinessRating2 = guestReview.Cleanliness;
                    RespectingRulesRating = guestReview.RespectingRules;
                    Comment2 = guestReview.Comment;
                }
            }
        }

        private AccommodationOwnerRating GetAccommodationOwnerRatingByReservationId(int reservationId)
        {
            foreach (AccommodationOwnerRating rating in accommodationOwnerRatingService.GetAll().Cast<AccommodationOwnerRating>())
            {
                if (rating.ReservationId == reservationId)
                {
                    return rating;
                }
            }

            return null;
        }

        private GuestReview GetGuestReviewByReservationId(int reservationId)
        {
            foreach (GuestReview review in guestReviewService.GetAll().Cast<GuestReview>())
            {
                // Pretpostavka: Pretpostavljamo da je ID rezervacije pohranjen u nekom od svojstava GuestReview objekta
                // Ovdje je samo primjer, prilagodite prema stvarnoj implementaciji
                if (review.AccommodationReservation.Id == reservationId)
                {
                    return review;
                }
            }

            return null;
        }
        public ObservableCollection<AccommodationReservation> ReservationItems
        {
            get { return reservationItems; }
            set
            {
                reservationItems = value;
                OnPropertyChanged(nameof(reservationItems));
            }
        }

        public void LoadData()
        {
            foreach (AccommodationReservation reservation in accommodationReservationService.GetAll().Cast<AccommodationReservation>())
            {
                if (reservation.GuestsRate && reservation.OwnersRate)
                {
                    ReservationItems.Add(reservation);
                }
            }
        }
    }
}
