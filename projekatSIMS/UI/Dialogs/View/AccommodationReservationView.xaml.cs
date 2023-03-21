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

            if (!IsDateRangeAvailable(startDate, endDate, selectedAccommodation, guestCount))
            {
                var newAvailableDateRanges = GetAllAvailableDateRanges(DateTime.Now, DateTime.Now.AddDays(10), selectedAccommodation, guestCount);
                AvailableDatesDataGrid.ItemsSource = newAvailableDateRanges;
                return;
            }

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
            ClearInput();

            var availableDateRanges = GetAllAvailableDateRanges(DateTime.Now, DateTime.Now.AddDays(10), selectedAccommodation, guestCount);
            AvailableDatesDataGrid.ItemsSource = availableDateRanges;
            ClearInput();
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

        private List<(DateTime, DateTime)> GetAllAvailableDateRanges(DateTime startDate, DateTime endDate, Accommodation selectedAccommodation, int guestCount)
        {
            startDate = startDate.Date;
            endDate = endDate.Date;

            var reservations = GetAccommodationReservations(selectedAccommodation);

            var allDateRanges = new List<(DateTime, DateTime)>();
            var currentStartDate = startDate;

            while (currentStartDate <= endDate)
            {
                var currentEndDate = GetCurrentEndDate(currentStartDate, selectedAccommodation, guestCount);

                if (currentEndDate > endDate)
                {
                    break;
                }

                if (IsAvailable(currentStartDate, currentEndDate, reservations))
                {
                    allDateRanges.Add((currentStartDate, currentEndDate));
                }

                currentStartDate = currentEndDate.AddDays(1);
            }

            return allDateRanges;
        }

        private IEnumerable<AccommodationReservation> GetAccommodationReservations(Accommodation selectedAccommodation)
        {
            return accommodationReservationService.GetAll()
                .OfType<AccommodationReservation>()
                .Where(r => r.AccommodationName == selectedAccommodation.Name);
        }

        private DateTime GetCurrentEndDate(DateTime currentStartDate, Accommodation selectedAccommodation, int guestCount)
        {
            return currentStartDate.AddDays((guestCount - 1) / selectedAccommodation.GuestLimit + 1);
        }

        private bool IsAvailable(DateTime currentStartDate, DateTime currentEndDate, IEnumerable<AccommodationReservation> reservations)
        {
            return !reservations.Any(r => IsOverlapping(currentStartDate, currentEndDate, r));
        }

        private bool IsOverlapping(DateTime currentStartDate, DateTime currentEndDate, AccommodationReservation reservation)
        {
            return currentStartDate <= reservation.EndDate && currentEndDate >= reservation.StartDate;
        }

        private bool IsDateRangeAvailable(DateTime startDate, DateTime endDate, Accommodation selectedAccommodation, int guestCount)
        {
            startDate = startDate.Date;
            endDate = endDate.Date;

            var reservations = GetAccommodationReservations(selectedAccommodation);

            foreach (var reservation in reservations)
            {
                if (IsOverlapping(startDate, endDate, reservation))
                {
                    var totalGuests = reservations.Sum(r => r.GuestCount);
                    if (guestCount > selectedAccommodation.GuestLimit - totalGuests)
                    {
                        var nextReservation = reservations
                            .Where(r => r.StartDate > endDate)
                            .OrderBy(r => r.StartDate)
                            .FirstOrDefault();

                        if (nextReservation != null)
                        {
                            var availableStartDate = nextReservation.StartDate;
                            var availableEndDate = availableStartDate.AddDays((endDate - startDate).TotalDays);
                            var message = $"Reservation unsuccessful. Guest limit achieved. Next available date range: {availableStartDate.ToShortDateString()} - {availableEndDate.ToShortDateString()}";
                            MessageBox.Show(message);
                        }
                        else
                        {
                            MessageBox.Show("Reservation unsuccessful. Guest limit achieved. No available date range found.");
                        }
                        return false;
                    }
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

            if ((endDate - startDate).TotalDays < selectedAccommodation.MinimalStay)
            {
                MessageBox.Show($"Minimum stay is {selectedAccommodation.MinimalStay} days.");
                return false;
            }

            return true;
        }
    }
}
