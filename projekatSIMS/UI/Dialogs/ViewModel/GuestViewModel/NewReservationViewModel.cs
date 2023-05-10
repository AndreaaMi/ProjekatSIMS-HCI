using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace projekatSIMS.UI.Dialogs.ViewModel.GuestViewModel
{
    public class NewReservationViewModel : ViewModelBase
    {
        //property pravim
        //Unututar propertija SelectedItems.selectedRenovation = taj property
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

        public ICommand ShowNewReservationHelpCommand { get; private set; }

        private ObservableCollection<Accommodation> accommodationItems = new ObservableCollection<Accommodation>();
        private ObservableCollection<string> accommodationCountryItems = new ObservableCollection<string>();
        private ObservableCollection<string> accommodationCityItems = new ObservableCollection<string>();
        private AccommodationService accommodationService;
        private AccommodationReservationService accommodationReservationService;

        public NewReservationViewModel()
        {
            ShowNewReservationHelpCommand = new RelayCommand(ShowNewReservationHelpControl);
            accommodationReservationService = new AccommodationReservationService();
            GuestCount = 1;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(1);
            SetService();
            LoadData();
        }

        private string _name;
        private int? _guestLimit;
        private int? _minimalStayDays;
        private AccommodationType? _propertyType;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _selectedCountry;
        public string SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                _selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        private string _selectedCity;
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
            }
        }

        public int? GuestLimit
        {
            get => _guestLimit;
            set
            {
                _guestLimit = value;
                OnPropertyChanged(nameof(GuestLimit));
            }
        }

        public int? MinimalStayDays
        {
            get => _minimalStayDays;
            set
            {
                _minimalStayDays = value;
                OnPropertyChanged(nameof(MinimalStayDays));
            }
        }

        public AccommodationType? PropertyType
        {
            get => _propertyType;
            set
            {
                _propertyType = value;
                OnPropertyChanged(nameof(PropertyType));
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

        private void FilterAccommodations()
        {
            var filteredAccommodations = AccommodationItems.Where(x =>
             (string.IsNullOrEmpty(Name) || x.Name.ToLower().Contains(Name.ToLower()))
             && (string.IsNullOrEmpty(SelectedCountry) || x.Location.Country.Equals(SelectedCountry, StringComparison.OrdinalIgnoreCase))
             && (string.IsNullOrEmpty(SelectedCity) || x.Location.City.Equals(SelectedCity, StringComparison.OrdinalIgnoreCase))
             && (!GuestLimit.HasValue || x.GuestLimit >= GuestLimit.Value)
             && (!MinimalStayDays.HasValue || x.MinimumStayDays >= MinimalStayDays.Value)
             && (!PropertyType.HasValue || x.Type == PropertyType.Value)
             ).ToList();

            // Update the collection of accommodations that should be displayed
            AccommodationItems.Clear();
            filteredAccommodations.ForEach(x => AccommodationItems.Add(x));
        }

        private void ShowNewReservationHelpControl(object parameter)
        {
            SelectedView = new NewReservationHelpView();
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

            foreach (Accommodation accommodation in accommodationService.GetAll().Cast<Accommodation>())
            {
                if (!AccommodationCountryItems.Contains(accommodation.Location.Country.ToString()))
                {
                    AccommodationCountryItems.Add(accommodation.Location.Country.ToString());
                }
            }

            foreach (Accommodation accommodation in accommodationService.GetAll().Cast<Accommodation>())
            {
                if (!AccommodationCityItems.Contains(accommodation.Location.City.ToString()))
                {
                    AccommodationCityItems.Add(accommodation.Location.City.ToString());
                }
            }
        }

        public void SetService()
        {
            accommodationService = new AccommodationService();
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

        public ObservableCollection<string> AccommodationCountryItems
        {
            get { return accommodationCountryItems; }
            set
            {
                accommodationCountryItems = value;
                OnPropertyChanged(nameof(accommodationCountryItems));
            }
        }

        public ObservableCollection<string> AccommodationCityItems
        {
            get { return accommodationCityItems; }
            set
            {
                accommodationCityItems = value;
                OnPropertyChanged(nameof(accommodationCityItems));
            }
        }

        private int guestCount;
        public int GuestCount
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
                GuestCount = GuestCount
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
            if (!int.TryParse(GuestCount.ToString(), out int guestCount) || guestCount < 1)
            {
                ReservationSuccessfulLabel = "";
                ErrorLabel = "Please enter the number of guests.";
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

