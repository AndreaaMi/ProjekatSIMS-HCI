using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace projekatSIMS.UI.Dialogs.ViewModel.GuestViewModel
{
    public class AnywhereAnytimeViewModel : ViewModelBase
    {
        public ICommand SearchCommand { get; set; }
        private ObservableCollection<Accommodation> accommodationItems = new ObservableCollection<Accommodation>();
        private AccommodationService accommodationService = new AccommodationService();
        private AccommodationReservationService accommodationReservationService = new AccommodationReservationService();

        public AnywhereAnytimeViewModel()
        { 
            SearchCommand = new RelayCommand(SearchAccommodations);
            LoadAccommodationItems();
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
                UpdateAvailableDates();
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
                UpdateAvailableDates();
            }
        }

        private int numberOfDays;
        public int NumberOfDays
        {
            get { return numberOfDays; }
            set
            {
                numberOfDays = value;
                OnPropertyChanged(nameof(NumberOfDays));
                UpdateAvailableDates();
            }
        }

        private bool isDateRangeSelected;
        public bool IsDateRangeSelected
        {
            get { return isDateRangeSelected; }
            set
            {
                isDateRangeSelected = value;
                OnPropertyChanged(nameof(IsDateRangeSelected));
                UpdateAvailableDates();
            }
        }

        private ObservableCollection<AvailableDate> availableDates = new ObservableCollection<AvailableDate>();
        public ObservableCollection<AvailableDate> AvailableDates
        {
            get { return availableDates; }
            set
            {
                availableDates = value;
                OnPropertyChanged(nameof(AvailableDates));
            }
        }

        private AvailableDate selectedDate;
        public AvailableDate SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
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

        private void LoadAccommodationItems()
        {
            foreach (Accommodation accommodation in accommodationService.GetAll())
            {
                AccommodationItems.Add(accommodation);
            }
        }

        private void UpdateAvailableDates()
        {
            AvailableDates.Clear();

            if (IsDateRangeSelected && StartDate != null && EndDate != null && NumberOfDays > 0)
            {
                foreach (var accommodation in AccommodationItems)
                {
                    if (IsAccommodationAvailable(accommodation))
                    {
                        DateTime currentDate = StartDate;

                        while (currentDate <= EndDate.AddDays(-NumberOfDays))
                        {
                            DateTime reservationEndDate = currentDate.AddDays(NumberOfDays);

                            if (IsAccommodationAvailableInDateRange(accommodation, currentDate, reservationEndDate))
                            {
                                AvailableDates.Add(new AvailableDate
                                {
                                    AvailableStartDate = currentDate,
                                    AvailableEndDate = reservationEndDate,
                                    AccommodationName = accommodation.Name
                                });
                            }

                            currentDate = currentDate.AddDays(1);
                        }
                    }
                }
            }
            //else if (!IsDateRangeSelected && GuestCount > 0)
            //{
            //    foreach (var accommodation in AccommodationItems)
            //    {
            //        if (accommodation.GuestLimit >= GuestCount)
            //        {
            //            AvailableDates.Add(new AvailableDate
            //            {
            //                AvailableStartDate = null,
            //                AvailableEndDate = null,
            //                AccommodationName = accommodation.Name
            //            });
            //        }
            //    }
           // }
        }

        private bool IsAccommodationAvailable(Accommodation accommodation)
        {
            foreach (AccommodationReservation accommodationReservation in accommodationReservationService.GetAll())
            {
                if (accommodationReservation.AccommodationName == accommodation.Name)
                {
                    if (accommodationReservation.StartDate <= EndDate && accommodationReservation.EndDate >= StartDate)
                    {
                        return false; // Smeštaj je zauzet u nekom od datuma
                    }
                }
            }
            return accommodation.GuestLimit >= GuestCount;
        }
        private bool IsAccommodationAvailableInDateRange(Accommodation accommodation, DateTime startDate, DateTime endDate)
        {
            foreach (AccommodationReservation accommodationReservation in accommodationReservationService.GetAll())
            {
                if (accommodationReservation.AccommodationName == accommodation.Name)
                {
                    if (accommodationReservation.StartDate <= endDate && accommodationReservation.EndDate >= startDate)
                    {
                        return false; // Smeštaj je zauzet u nekom od datuma
                    }
                }
            }
            return true; // Smeštaj je dostupan za dati opseg datuma
        }

        private void SearchAccommodations(object parameter)
        {
            if (GuestCount <= 0 || NumberOfDays <= 0)
            {
                MessageBox.Show("Morate uneti broj gostiju i broj dana boravka.");
                return;
            }

            AvailableDates.Clear();

            foreach (var accommodation in AccommodationItems)
            {
                if (accommodation.GuestLimit >= GuestCount)
                {
                    AvailableDates.Add(new AvailableDate
                    {
                        AvailableStartDate = StartDate,
                        AvailableEndDate = StartDate.AddDays(NumberOfDays),
                        AccommodationName = accommodation.Name
                    });
                }
            }
            AccommodationItems.Clear();

            foreach (var accommodation in AccommodationItems)
            {
                if (accommodation.GuestLimit < GuestCount)
                {
                    AccommodationItems.Add(accommodation);
                }
            }

        }

        public ICommand ShowAnywhereAnytimeHelpCommand { get; set; }
        public ICommand BackCommand { get; set; }
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

        // BackCommand = new RelayCommand(BackControl);
        // ShowAnywhereAnytimeHelpCommand = new RelayCommand(ShowAnywhereAnytimeHelpControl);

    }

}
