﻿using projekatSIMS.Model;
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
        private readonly ReservationRescheduleRequestService reservationRescheduleRequestService;
        private Accommodation selectedAccommodation;
        private AccommodationReservation selectedReservation;
        private DateTime startDate;
        private DateTime endDate;
        private int guestCount;

        public AccommodationReservationView()
        {
            InitializeComponent();

            accommodationService = new AccommodationService();
            accommodationReservationService = new AccommodationReservationService();
            reservationRescheduleRequestService = new ReservationRescheduleRequestService();

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
            ReservationsListView.Items.Clear();
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

    ClearInput();

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
                    break;
                }
                currentStartDate = currentEndDate.AddDays(1);
                if (currentStartDate > DateTime.MaxValue.AddDays(-selectedAccommodation.MinimalStay))
                {
                    break;
                }
                currentEndDate = currentStartDate.AddDays(dateDifference);
                reservations = GetAccommodationReservations(accommodation, currentStartDate, currentEndDate);
            }

            if (availableDates.Count == 0)
            {
                MessageBox.Show("No available dates found");
            }
            else if (availableDates[0].AvailableStartDate != startDate || availableDates[0].AvailableEndDate != endDate)
            {
                MessageBox.Show("The selected dates are not available. Here are some alternative dates:");
                availableDates = GetAvailableDates(selectedAccommodation);
                if (availableDates.Count == 0)
                {
                    MessageBox.Show("No available dates found");
                    
                }
            }
            else
            {
                MessageBox.Show("Reservation successful");
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

            // Check for available dates
            List<AvailableDate> availableDates = GetAvailableDates(selectedAccommodation);
            if (availableDates.Count == 0)
            {
                MessageBox.Show("No available dates found");
                return false;
            }

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

        private bool ValidateReservationSelection()
        {
            if (ReservationsListView.SelectedItem == null)
            {
                MessageBox.Show("Please select reservation.");
                return false;
            }

            int selectedIndex = ReservationsListView.SelectedIndex;
            var reservations = accommodationReservationService.GetAll().OfType<AccommodationReservation>().ToList();

            selectedReservation = new AccommodationReservation
            {
                Id = reservations[selectedIndex].Id,
                AccommodationName = reservations[selectedIndex].AccommodationName,
                StartDate = reservations[selectedIndex].StartDate,
                EndDate = reservations[selectedIndex].EndDate,
                GuestCount = reservations[selectedIndex].GuestCount
            };

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
            if (IsReservationOverlapping(startDate, endDate))
            {
                MessageBox.Show("This date range overlaps with an existing reservation.");
                return false;
            }

            if ((endDate - startDate).TotalDays < selectedAccommodation.MinimalStay)
            {
                MessageBox.Show($"Minimum stay is {selectedAccommodation.MinimalStay} days.");
                return false;
            }

            return true;
        }
        private bool IsReservationOverlapping(DateTime startDate, DateTime endDate)
        {
            foreach (AccommodationReservation entity in accommodationReservationService.GetAll().Cast<AccommodationReservation>())
            {
                if (entity.AccommodationName == selectedAccommodation.Name && startDate < entity.EndDate && endDate > entity.StartDate)
                {
                    return true;
                }
            }
            return false;
        }

        private void ShowAllRequestRescheduleButton_Click(object sender, RoutedEventArgs e)
        {
            ReservationRescheduleRequestService reservationRescheduleRequestService = new ReservationRescheduleRequestService();
            var allRequests = reservationRescheduleRequestService.GetAll();

            RequestReservationRescheduleViewList.ItemsSource = allRequests;
        }

        private void ReservationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReservationsListView.SelectedItem != null){
                RequestRescheduleButton.IsEnabled = true;            
            }
            else{
                RequestRescheduleButton.IsEnabled = false;
            }
        }

        private void RequestRescheduleButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateReservationSelection()) return;
            ReservationRescheduleRequest request = new ReservationRescheduleRequest
            {
                Id = reservationRescheduleRequestService.GenerateId(),
                ReservationId = selectedReservation.Id,
                NewStartDate = (DateTime)NewStartDatePicker.SelectedDate,
                NewEndDate = (DateTime)NewEndDatePicker.SelectedDate,
                GuestName = "Andrea",
                Status = RequestStatusType.PENDING
            };

            reservationRescheduleRequestService.Add(request);

            MessageBox.Show("The request has been sent to the owner. You will be notified of the status of the request.");
            LoadReservations();
            ReservationsListView.SelectedItem = selectedReservation;
        }

        public int GetCancelationDays(Accommodation accommodation)
        {
            return accommodation.CancelationLimit;
        }
        private bool ValidateReservation()
        {
            var selectedAccommodation = accommodationService.GetAccommodationByName(selectedReservation.AccommodationName);
            var cancelationLimit = selectedAccommodation.CancelationLimit;

            if (!ValidateReservationSelection()) return false; 
            
            if (selectedReservation == null){
                MessageBox.Show("Invalid reservation selected");
                return false;
            }

            if (selectedAccommodation == null){
                MessageBox.Show($"Accommodation for reservation {selectedReservation.Id} not found");
                return false;
            }

            if (cancelationLimit > 0)
            {
                var daysUntilStart = (selectedReservation.StartDate - DateTime.Today).Days;
                if (daysUntilStart <= cancelationLimit){
                    MessageBox.Show($"Sorry, you cannot cancel this reservation as the cancellation limit is {cancelationLimit} days");
                    return false;
                }
            }

            return true;
        }

        private void RemoveReservation()
        {
            foreach (AccommodationReservation entity in accommodationReservationService.GetAll())
            {
                if (entity.Id == selectedReservation.Id)
                {
                    accommodationReservationService.Remove(entity);
                    break;
                }
            }
            ReservationsListView.Items.Remove(selectedReservation);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateReservation()) return;

            RemoveReservation();

            MessageBox.Show("Reservation canceled successfully");
            LoadReservations();
        }
    }
}
