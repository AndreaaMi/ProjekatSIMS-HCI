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

        private ObservableCollection<ReservationRescheduleRequest> reservationRescheduleItems = new ObservableCollection<ReservationRescheduleRequest>();
        private ObservableCollection<AccommodationOwnerRating> ratingItems = new ObservableCollection<AccommodationOwnerRating>();

        private ReservationRescheduleRequestService reservationRescheduleRequestService;
        private AccommodationOwnerRatingService ratingService;

        public GuestPageViewModel()
        {

            ShowActiveReservationCommand = new RelayCommand(ShowActiveReservation);
            SetService();
            LoadData();

        }
        private void LoadData()
        {
            InitialListViewLoad();
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
 
        }
        public void SetService()
        {
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

        private void ShowActiveReservation(object parameter)
        {
            SelectedView = new ActiveReservationsView();
        }
    }
}