using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace projekatSIMS.UI.Dialogs.ViewModel.GuestViewModel
{
    public class AnywhereAnytimeViewModel : ViewModelBase
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

        public ICommand ShowAnywhereAnytimeHelpCommand { get; set; }
        public ICommand BackCommand { get; set; }
        private ObservableCollection<Accommodation> accommodationItems = new ObservableCollection<Accommodation>();
        private AccommodationService accommodationService;
        private AccommodationReservationService accommodationReservationService;


        public AnywhereAnytimeViewModel()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(1);
            BackCommand = new RelayCommand(BackControl);
            ShowAnywhereAnytimeHelpCommand = new RelayCommand(ShowAnywhereAnytimeHelpControl);
            SetService();
            LoadData();
        }

        private Accommodation selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get { return selectedAccommodation; }
            set
            {
                selectedAccommodation = value;
                OnPropertyChanged(nameof(SelectedAccommodation));
            }
        }

        private ICommand _showAccommodationsCommand;
        public ICommand ShowAccommodationsCommand
        {
            get
            {
                if (_showAccommodationsCommand == null)
                {
                    _showAccommodationsCommand = new RelayCommand(
                        param => this.FilterAccommodations(),
                        param => true
                    );
                }
                return _showAccommodationsCommand;
            }
        }

        private ICommand bookCommand;
        public ICommand BookCommand
        {
            get
            {
                if (bookCommand == null)
                {
                    bookCommand = new RelayCommand(
                        param => BookAccommodation(),
                        param => true
                    );
                }
                return bookCommand;
            }
        }


        public ObservableCollection<Accommodation> AccommodationItems
        {
            get { return accommodationItems; }
            set
            {
                accommodationItems = value;
                OnPropertyChanged(nameof(accommodationItems));
            }
        }


        private void LoadData()
        {
            InitialListViewLoad();
        }

        private void InitialListViewLoad()
        {
            foreach (Accommodation accommodation in accommodationService.GetAll().Cast<Accommodation>())
            {
                AccommodationItems.Add(accommodation);
            }
        }

        public void SetService()
        {
            accommodationService = new AccommodationService();
            accommodationReservationService = new AccommodationReservationService();
        }


        private void BackControl(object parameter)
        {
            SelectedView = new GuestPageView();
        }
        private void ShowAnywhereAnytimeHelpControl(object parameter)
        {
            SelectedView = new AnywhereAnytimeHelpView();
        }

        private int? guestCount;
        public int? GuestCount
        {
            get { return guestCount; }
            set
            {
                guestCount = value;
                OnPropertyChanged(nameof(GuestCount));
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private int? minimalStayDays;

        public int? MinimalStayDays
        {
            get => minimalStayDays;
            set
            {
                minimalStayDays = value;
                OnPropertyChanged(nameof(MinimalStayDays));
            }
        }

        private void FilterAccommodations()
        {
            var filteredAccommodations = AccommodationItems.Where(x =>
                (!GuestCount.HasValue || x.GuestLimit >= GuestCount.Value)
                && (!MinimalStayDays.HasValue || x.MinimumStayDays <= MinimalStayDays.Value)
            ).ToList();

            // Update the collection of accommodations that should be displayed
            AccommodationItems.Clear();
            filteredAccommodations.ForEach(x => AccommodationItems.Add(x));
            CommandManager.InvalidateRequerySuggested();

        }

        private void BookAccommodation()
        {
            if (!ValidateInput())
            {
                return; // do not book the accommodation if input is not valid
            }

            var newReservation = new AccommodationReservation
            {
                Id = accommodationReservationService.GenerateId(),
                AccommodationName = SelectedAccommodation.Name,
                StartDate = StartDate,
                EndDate = EndDate,
                GuestCount = (int)GuestCount
            };

            accommodationReservationService.CreateAccommodationReservation(newReservation);

            ReservationSuccessfulLabel = "Reservation successful!";
            ErrorLabel = "";
        }

        private string reservationSuccessfulLabel;
        public string ReservationSuccessfulLabel
        {
            get { return reservationSuccessfulLabel; }
            set
            {
                reservationSuccessfulLabel = value;
                OnPropertyChanged(nameof(ReservationSuccessfulLabel));
            }
        }

        private bool ValidateInput()
        {
            if (!ValidateAccommodationSelection()) return false;
            if (!ValidateDateRange()) return false;
            if (!ValidateGuestCount()) return false;
            return true;
        }

        private bool ValidateAccommodationSelection()
        {
            if (SelectedAccommodation == null)
            {
                ReservationSuccessfulLabel = "";
                ErrorLabel = "Please select an accommodation.";
                return false;
            }
            ErrorLabel = "";
            return true;
        }

        private bool ValidateDateRange()
        {
            string dateFormat = "dd.MM.yyyy"; // Prilagodite formatu datuma vašim potrebama

            if (!DateTime.TryParseExact(StartDate.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                ReservationSuccessfulLabel = "";
                ErrorLabel = $"Please enter a valid start date in the format {dateFormat}.";
                return false;
            }

            if (!DateTime.TryParseExact(EndDate.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                ReservationSuccessfulLabel = "";
                ErrorLabel = $"Please enter a valid end date in the format {dateFormat}.";
                return false;
            }

            if (StartDate == DateTime.MinValue || EndDate == DateTime.MaxValue || StartDate >= EndDate || StartDate < DateTime.Today.AddDays(1))
            {
                ReservationSuccessfulLabel = "";
                ErrorLabel = "Please select a valid date range.";
                return false;
            }

            if ((EndDate - StartDate).TotalDays < SelectedAccommodation.MinimumStayDays)
            {
                ReservationSuccessfulLabel = "";
                ErrorLabel = $"Minimum stay is {SelectedAccommodation.MinimumStayDays} days.";
                return false;
            }
            return true;
        }

        private bool ValidateGuestCount()
        {
            if (!int.TryParse(GuestCount.ToString(), out int guestCount))
            {
                ReservationSuccessfulLabel = "";
                ErrorLabel = "Please enter a valid number of guests. The number of guests must be a whole number.";
                return false;
            }

            if (guestCount < 1)
            {
                ReservationSuccessfulLabel = "";
                ErrorLabel = "Please enter a valid number of guests. The number of guests must be greater than zero.";
                return false;
            }

            if (guestCount > SelectedAccommodation.GuestLimit)
            {
                ReservationSuccessfulLabel = "";
                ErrorLabel = $"Selected accommodation only allows up to {SelectedAccommodation.GuestLimit} guests.";
                return false;
            }

            return true;
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
    }
}
