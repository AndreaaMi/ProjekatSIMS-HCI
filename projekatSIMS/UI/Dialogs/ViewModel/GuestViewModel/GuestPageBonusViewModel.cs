using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace projekatSIMS.UI.Dialogs.ViewModel
{
    public class GuestPageBonusViewModel : ViewModelBase
    {
        private UserControl _selectedView;

        public UserControl SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                OnPropertyChanged(nameof(SelectedView));
            }
        }
        public ICommand ShowActiveReservationCommand { get; private set; }
        public ICommand ShowRateNowCommand { get; private set; }

        private ObservableCollection<ReservationRescheduleRequest> reservationRescheduleItems = new ObservableCollection<ReservationRescheduleRequest>();
        private ObservableCollection<AccommodationOwnerRating> ratingItems = new ObservableCollection<AccommodationOwnerRating>();
        public ObservableCollection<AccommodationReservation> reservationItems = new ObservableCollection<AccommodationReservation>();

        private AccommodationReservationService accommodationReservationService;

        private ReservationRescheduleRequestService reservationRescheduleRequestService;
        private AccommodationOwnerRatingService ratingService;

        public ICommand ShowRatedReservationsCommand { get; set; }
        public ICommand ShowNotRatedReservationsCommand { get; set; }
        public ICommand ShowDisplayReviewsViewCommand { get; set; }

        public UserService userService;

        public GuestPageBonusViewModel()
        {

            userService = new UserService();
            userId = userService.GetCurrentUserId();
            SetService();
            LoadData();
            UseBonusPointCommand = new RelayCommand(UseBonusPoint);
            ShowDisplayReviewsViewCommand = new RelayCommand(ShowDisplayReviewsControl);
            ShowRateNowCommand = new RelayCommand(ShowRateNowControl);
            ShowRatedReservationsCommand = new RelayCommand(ShowRatedReservations);
            ShowNotRatedReservationsCommand = new RelayCommand(ShowNotRatedReservations);
            ShowActiveReservationCommand = new RelayCommand(ShowActiveReservation);
            IsRatedSelected = false;
            userService.UpdateSuperGuestStatus(userId, ReservationCount);

        }

        private bool isRatedSelected;
        public bool IsRatedSelected
        {
            get { return isRatedSelected; }
            set
            {
                isRatedSelected = value;
                OnPropertyChanged(nameof(IsRatedSelected));
                OnPropertyChanged(nameof(IsNotRatedSelected));
            }
        }

        public bool IsNotRatedSelected
        {
            get { return !isRatedSelected; }
        }
        private void LoadData()
        {
            User currentUser = userService.GetLoginUser();

            if (currentUser != null)
            {
                IsSuperGuest = currentUser.IsSuperGuest;
                ReservationCount = currentUser.ReservationCount;
                BonusPoints = currentUser.BonusPoints;
                SuperGuestExpirationDate = currentUser.SuperGuestExpirationDate;
            }
            InitialListViewLoad();
        }
        private void ShowRatedReservations(object parameter)
        {
            ReservationItems = new ObservableCollection<AccommodationReservation>(ReservationItems.Where(r => r.GuestsRate));
            IsRatedSelected = true;
        }

        private void ShowNotRatedReservations(object parameter)
        {
            ReservationItems = new ObservableCollection<AccommodationReservation>(ReservationItems.Where(r => !r.GuestsRate));
            IsRatedSelected = false;
        }

        private void InitialListViewLoad()
        {

            foreach (ReservationRescheduleRequest request in reservationRescheduleRequestService.GetAll().Cast<ReservationRescheduleRequest>())
            {
                ReservationRescheduleItems.Add(request);
            }
            foreach (AccommodationOwnerRating rating in ratingService.GetAll().Cast<AccommodationOwnerRating>())
            {
                RatingItems.Add(rating);
            }
            foreach (AccommodationReservation reservation in accommodationReservationService.GetAll().Cast<AccommodationReservation>())
            {
                ReservationItems.Add(reservation);
            }

        }
        public void SetService()
        {
            accommodationReservationService = new AccommodationReservationService();
            reservationRescheduleRequestService = new ReservationRescheduleRequestService();
            ratingService = new AccommodationOwnerRatingService();
        }


        public ObservableCollection<ReservationRescheduleRequest> ReservationRescheduleItems
        {
            get { return reservationRescheduleItems; }
            set
            {
                reservationRescheduleItems = value;
                OnPropertyChanged(nameof(ReservationRescheduleItems));
            }
        }

        public ObservableCollection<AccommodationOwnerRating> RatingItems
        {
            get { return ratingItems; }
            set
            {
                ratingItems = value;
                OnPropertyChanged(nameof(RatingItems));
            }
        }

        public ObservableCollection<AccommodationReservation> ReservationItems
        {
            get { return reservationItems; }
            set
            {
                reservationItems = value;
                OnPropertyChanged(nameof(ReservationItems));
            }
        }

        private void ShowActiveReservation(object parameter)
        {
            SelectedView = new ActiveReservationsView();
        }

        private void ShowRateNowControl(object parameter)
        {
            SelectedView = new RateNowView();
        }

        private void ShowDisplayReviewsControl(object parameter)
        {
            SelectedView = new DisplayReviewsView();
        }


        private DateTime superGuestExpirationDate;
        private int userId;
        private bool isSuperGuest;
        public bool IsSuperGuest
        {
            get { return isSuperGuest; }
            set
            {
                isSuperGuest = value;
                OnPropertyChanged(nameof(IsSuperGuest));
            }
        }

        private int reservationCount;
        public int ReservationCount
        {
            get { return reservationCount; }
            set
            {
                reservationCount = value;
                OnPropertyChanged(nameof(ReservationCount));
            }
        }

        private int bonusPoints;
        public int BonusPoints
        {
            get { return bonusPoints; }
            set
            {
                bonusPoints = value;
                OnPropertyChanged(nameof(BonusPoints));
            }
        }

        public ICommand UseBonusPointCommand { get; set; }


        public DateTime SuperGuestExpirationDate
        {
            get { return superGuestExpirationDate; }
            set
            {
                superGuestExpirationDate = value;
                OnPropertyChanged(nameof(SuperGuestExpirationDate));
            }
        }

        private void UseBonusPoint(object parameter)
        {
            bool success = userService.UseBonusPoint(userId);
            if (success)
            {
                SuccessLabel = "Uspešno ste iskorisitli bonus poen!";
                ErrorLabel = "";
            }
            else
            {
                ErrorLabel = "Nemoguće iskoristiti bonus poen.";
                SuccessLabel = "";
            }

        }

        private string errorLabel;
        public string ErrorLabel
        {
            get { return errorLabel; }
            set
            {
                errorLabel = value;
                OnPropertyChanged(nameof(ErrorLabel));
            }
        }

        private string successLabel;
        public string SuccessLabel
        {
            get { return successLabel; }
            set
            {
                successLabel = value;
                OnPropertyChanged(nameof(SuccessLabel));
            }
        }
    }
}

