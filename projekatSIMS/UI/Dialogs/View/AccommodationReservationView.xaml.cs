using projekatSIMS.Model;
using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace projekatSIMS.UI.Dialogs.View
{
    /// <summary>
    /// Interaction logic for AccommodationReservationView.xaml
    /// </summary>
    public partial class AccommodationReservationView : Window
    {
        private readonly AccommodationService accommodationService;
        private readonly AccommodationReservationService accommodationReservationService;
        private Accommodation selectedAccommodation;
        private DateTime startDate;
        private DateTime endDate;
        private int guestCount;

        public AccommodationReservationView()
        {
            InitializeComponent();

            accommodationService = new AccommodationService();
            accommodationReservationService = new AccommodationReservationService();

            LoadAccommodations();
            LoadReservations();
        }

        private void LoadAccommodations()
        {
            foreach (Accommodation accommodation in accommodationService.GetAll().OfType<Accommodation>())
            {
                AccommodationDataGrid.Items.Add(accommodation);
            }
        }

        private void LoadReservations()
        {
            foreach (AccommodationReservation reservation in accommodationReservationService.GetAll().OfType<AccommodationReservation>())
            {
                ReservationsListView.Items.Add(new
                {
                    reservation.Id,
                    reservation.AccommodationName,
                    StartDate = reservation.StartDate.ToString("dd-MM-yyyy"),
                    EndDate = reservation.EndDate.ToString("dd-MM-yyyy"),
                    reservation.GuestCount
                });
            }
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;

            var newReservation = new AccommodationReservation
            {
                Id = accommodationReservationService.GenerateId(),
                AccommodationName = selectedAccommodation.Name,
                StartDate = startDate,
                EndDate = endDate,
                GuestCount = guestCount
            };

            accommodationReservationService.CreateAccommodationReservation(newReservation);
            MessageBox.Show("Reservation successful");
            ReservationsListView.Items.Add(newReservation);

            List<AvailableDate> availableDates = GetAvailableDates(selectedAccommodation);
            if (availableDates.Count > 0)
            {
                AvailableDatesDataGrid.ItemsSource = availableDates;
            }
            else
            {
                MessageBox.Show("No available dates found");
            }

            ClearInput();
        }

        private List<AvailableDate> GetAvailableDates(Accommodation accommodation)
        {
            List<AvailableDate> availableDates = new List<AvailableDate>();
            DateTime currentStartDate = startDate;
            DateTime currentEndDate = endDate;
            TimeSpan diff = endDate - startDate;
            int dateDifference = (int)diff.TotalDays;
            IEnumerable<AccommodationReservation> reservations = GetAccommodationReservations(accommodation, currentStartDate, currentEndDate);

            while (currentStartDate <= currentEndDate)
            {
                if (IsAvailable(currentStartDate, currentEndDate, reservations))
                {
                    availableDates.Add(new AvailableDate
                    {
                        AvailableStartDate = currentStartDate,
                        AvailableEndDate = currentEndDate,
                        AccommodationName = selectedAccommodation.Name
                    });
                }
                currentStartDate = currentEndDate.AddDays(1);
                if (currentStartDate > DateTime.MaxValue.AddDays(-selectedAccommodation.MinimalStay))
                {
                    break;
                }
                currentEndDate = currentStartDate.AddDays(dateDifference);
            }
            return availableDates;
        }

        private bool ValidateGuestCount()
        {
            if (!int.TryParse(GuestCountTextBox.Text, out guestCount) || guestCount < 1)
            {
                MessageBox.Show("Please enter a valid guest count.");
                return false;
            }


            return true;
        }
        private IEnumerable<AccommodationReservation> GetAccommodationReservations(Accommodation accommodation, DateTime startDate, DateTime endDate)
        {
            return accommodationReservationService.GetAll()
                .OfType<AccommodationReservation>()
                .Where(r => r.AccommodationName == accommodation.Name &&
                            r.StartDate <= endDate && r.EndDate >= startDate);
        }

        private bool IsAvailable(DateTime currentStartDate, DateTime currentEndDate, IEnumerable<AccommodationReservation> reservations)
        {
            foreach (var reservation in reservations)
            {
                if (currentStartDate >= reservation.StartDate && currentEndDate <= reservation.EndDate)
                {
                    return false;
                }
            }
            return true;
        }
        private void ClearInput()
        {
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
            GuestCountTextBox.Text = string.Empty;
            MinimalStayLabel.Content = string.Empty;
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
            if (AccommodationDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an accommodation.");
                return false;
            }

            selectedAccommodation = (Accommodation)AccommodationDataGrid.SelectedItem;
            MinimalStayLabel.Content = $"Minimal stay: {selectedAccommodation.MinimalStay} days";

            return true;
        }

        private bool ValidateDateRange()
        {
            startDate = (DateTime)StartDatePicker.SelectedDate;
            endDate = (DateTime)EndDatePicker.SelectedDate;

            if (startDate == DateTime.MinValue || endDate == DateTime.MaxValue || startDate >= endDate || startDate < DateTime.Today.AddDays(1))
            {
                MessageBox.Show("Please select a valid date range.");
                return false;
            }

            // Check for overlapping reservations
            AccommodationReservationService accommodationReservationService = new AccommodationReservationService();

            foreach (AccommodationReservation entity in accommodationReservationService.GetAll().Cast<AccommodationReservation>())
            {
                if (entity.AccommodationName == selectedAccommodation.Name && startDate < entity.EndDate && endDate > entity.StartDate)
                {
                    MessageBox.Show("This date range overlaps with an existing reservation.");
                    return false;
                }
            }  


            if ((endDate - startDate).TotalDays < selectedAccommodation.MinimalStay)
            {
                MessageBox.Show($"Minimum stay is {selectedAccommodation.MinimalStay} days.");
                return false;
            }

            return true;
        }

        private void ShowAllRequestRescheduleButton_Click(object sender, RoutedEventArgs e)
        {
            ReservationRescheduleRequestService reservationRescheduleRequestService = new ReservationRescheduleRequestService();
            var allRequests = reservationRescheduleRequestService.GetAll();

            RequestReservationRescheduleViewList.ItemsSource = allRequests;
        }

        private void ReservationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReservationsListView.SelectedItem != null)
            {
                RequestRescheduleButton.IsEnabled = true;            }
            else
            {
                RequestRescheduleButton.IsEnabled = false;
            }
        }
    }
}
