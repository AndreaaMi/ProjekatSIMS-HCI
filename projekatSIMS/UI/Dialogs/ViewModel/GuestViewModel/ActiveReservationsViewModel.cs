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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace projekatSIMS.UI.Dialogs.ViewModel.GuestViewModel
{
    public class ActiveReservationsViewModel : ViewModelBase
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

        public ICommand ShowNewReservationsCommand { get; private set; }

        public ICommand ShowActiveReservationsHelpCommand { get; private set; }


        private ObservableCollection<AccommodationReservation> reservationItems = new ObservableCollection<AccommodationReservation>();
        
        private AccommodationService accommodationService;
       
        private  AccommodationReservationService accommodationReservationService;
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }

        public ICommand CancelReservationCommand { get; set; }
        public ActiveReservationsViewModel()
        {
            accommodationService = new AccommodationService();
            accommodationReservationService = new AccommodationReservationService();

            Reservations = new ObservableCollection<AccommodationReservation>(); 

            ShowActiveReservationsHelpCommand = new RelayCommand(ShowActiveReservationsHelpControl);
            ShowNewReservationsCommand = new RelayCommand(ShowNewReservations);
            SetService();
            LoadData();

            CancelReservationCommand = new RelayCommand(CancelReservation);
            CancelReservationCommand = new RelayCommand(CancelReservation);
        }

        private void LoadData()
        {
            InitialListViewLoad();
        }

        private void InitialListViewLoad()
        {

            foreach (AccommodationReservation reservation in accommodationReservationService.GetAll().Cast<AccommodationReservation>())
            {
                ReservationItems.Add(reservation);
            }
           
        }

        private string _cancelMessage;
        public string CancelMessage
        {
            get { return _cancelMessage; }
            set
            {
                _cancelMessage = value;
                OnPropertyChanged(nameof(CancelMessage));
            }
        }

        private void CancelReservation(object parameter)
        {
            if (!ValidateReservation()) return;

            RemoveReservation();

            CancelMessage = "Reservation canceled successfully";

            LoadData();
        }

        private bool ValidateReservation()
        {
            if (SelectedReservation == null)
            {
                CancelMessage = "Invalid reservation selected";
                return false;
            }

            var selectedAccommodation = accommodationService.GetAccommodationByName(SelectedReservation.AccommodationName);

            if (selectedAccommodation == null)
            {
                CancelMessage = $"Accommodation for reservation {SelectedReservation.Id} not found";
                return false;
            }

            var cancelationLimit = selectedAccommodation.CancellationDays;

            if (cancelationLimit > 0)
            {
                var daysUntilStart = (SelectedReservation.StartDate - DateTime.Today).Days;
                if (daysUntilStart <= cancelationLimit)
                {
                    CancelMessage = $"Sorry, you cannot cancel this reservation as the cancellation limit is {cancelationLimit} days";
                    return false;
                }
            }

            return true;
        }

        private void RemoveReservation()
        {
            foreach (AccommodationReservation entity in accommodationReservationService.GetAll())
            {
                if (entity.Id == SelectedReservation.Id)
                {
                    accommodationReservationService.Remove(entity);
                    break;
                }
            }
            if (SelectedReservation != null)
            {
                Reservations.Remove(SelectedReservation);
            }
        }



        public void SetService()
        {
            accommodationReservationService = new AccommodationReservationService();
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

        private void ShowNewReservations(object parameter)
        {
            SelectedView = new NewReservationView();
        }

        private void ShowActiveReservationsHelpControl(object parameter)
        {
            SelectedView = new ActiveReservationsHelpView();
        }
    }
}
