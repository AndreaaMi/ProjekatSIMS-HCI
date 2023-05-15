using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using projekatSIMS.UI.Dialogs.View.GuestView;

namespace projekatSIMS.UI.Dialogs.ViewModel.GuestViewModel
{
    public class RequestNewDateViewModel : ViewModelBase
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

        public ICommand ShowRequestNewDateHelpViewCommand { get; private set; }
        public ICommand BackCommand { get; private set; }


        private AccommodationReservation _selectedReservation;
        private DateTime _newStartDate;
        private DateTime _newEndDate;
        private readonly ReservationRescheduleRequestService reservationRescheduleRequestService;
        private ObservableCollection<AccommodationReservation> reservationItems = new ObservableCollection<AccommodationReservation>();
        
        private AccommodationReservationService accommodationReservationService;
        private ObservableCollection<string> reservationItemId = new ObservableCollection<string>();

        public RequestNewDateViewModel()
        {
            BackCommand = new RelayCommand(BackControl);
            ShowRequestNewDateHelpViewCommand = new RelayCommand(ShowRequestNewDateHelpViewControl);
            accommodationReservationService = new AccommodationReservationService();
            NewStartDate = DateTime.Now;
            NewEndDate = DateTime.Now.AddDays(1);
            reservationRescheduleRequestService = new ReservationRescheduleRequestService();
            LoadData();
        }

        private void ShowRequestNewDateHelpViewControl(object parameter)
        {
            SelectedView = new RequestNewDateHelpView();
        }

        private void BackControl(object parameter)
        {
            SelectedView = new ActiveReservationsView();
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

        public ObservableCollection<string> ReservationNamesItems
        {
            get => reservationItemId;
            set
            {
                reservationItemId = value;
                OnPropertyChanged(nameof(reservationItemId));
            }
        }

        public void LoadData()
        {
            foreach (AccommodationReservation reservation in accommodationReservationService.GetAll().Cast<AccommodationReservation>())
            {
                   ReservationItems.Add(reservation);
            }

            foreach (AccommodationReservation reservation1 in accommodationReservationService.GetAll().Cast<AccommodationReservation>())
            {
                if(!ReservationNamesItems.Contains(reservation1.AccommodationName))
                {
                    ReservationNamesItems.Add(reservation1.AccommodationName);
                }
            }

        }

        public DateTime NewStartDate
        {
            get => _newStartDate;
            set
            {
                _newStartDate = value;
                OnPropertyChanged(nameof(NewStartDate));
            }
        }

        public DateTime NewEndDate
        {
            get => _newEndDate;
            set
            {
                _newEndDate = value;
                OnPropertyChanged(nameof(NewEndDate));
            }
        }

        public AccommodationReservation SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                _selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
            }
        }

        public ICommand RequestRescheduleCommand => new RelayCommand(RequestReschedule);

        private void RequestReschedule(object parameter)
        {
         if (SelectedReservation == null) {
                SuccessfulLabel = "";
                ErrorLabel= "Please select an reservation.";
                return;
            }
            if (NewStartDate == DateTime.Now || NewEndDate == DateTime.Now.AddDays(1))
            {
                SuccessfulLabel = "";
                ErrorLabel = "Please select a new date range.";
                return;
            }

            ReservationRescheduleRequest request = new ReservationRescheduleRequest
            {
                Id = reservationRescheduleRequestService.GenerateId(),
                ReservationId = SelectedReservation.Id,
                NewStartDate = NewStartDate,
                NewEndDate = NewEndDate,
                GuestName = "Andrea",
                Status = RequestStatusType.PENDING,
                Comment = null
            };

            reservationRescheduleRequestService.Add(request);

            ErrorLabel = "";
            SuccessfulLabel = "The request has been sent to the owner. You will be notified of the status of the request.";
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

        private string successfulLabel;
        public string SuccessfulLabel
        {
            get { return successfulLabel; }
            set
            {
                successfulLabel = value;
                OnPropertyChanged(nameof(SuccessfulLabel));
            }
        }
    }
}