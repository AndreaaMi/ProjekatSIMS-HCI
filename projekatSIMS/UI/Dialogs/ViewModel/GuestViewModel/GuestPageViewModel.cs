using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.GuestView;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace projekatSIMS.UI.Dialogs.ViewModel.GuestViewModel
{
    public class GuestPageViewModel : ViewModelBase
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


        public GuestPageViewModel()
        {
            ShowRateNowCommand = new RelayCommand(ShowRateNowControl);
            ShowRatedReservationsCommand = new RelayCommand(ShowRatedReservations);
            ShowNotRatedReservationsCommand = new RelayCommand(ShowNotRatedReservations);
            ShowActiveReservationCommand = new RelayCommand(ShowActiveReservation);
            SetService();
            LoadData();
            IsRatedSelected = false;

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
    }
}