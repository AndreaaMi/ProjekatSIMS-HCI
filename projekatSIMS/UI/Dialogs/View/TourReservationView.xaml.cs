using projekatSIMS.Model;
using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    /// Interaction logic for TourReservationView.xaml
    /// </summary>
    public partial class TourReservationView : Window
    {
        private readonly TourService tourService;
        private readonly TourReservationService tourReservationService;
        public Tour selectedTour;

        private int guestCount;
        private int tourId;

        public TourReservationView()
        {
            InitializeComponent();

            tourService = new TourService();
            tourReservationService = new TourReservationService();

            LoadTours();
            LoadReservations();
        }

        private void LoadTours()
        {
            foreach (Tour item in tourService.GetAll())
            {
                TourDataGrid.Items.Add(item);
            }
        }

        private void LoadReservations()
        {
            foreach (TourReservation item in tourReservationService.GetAll())
            {
                ReservationsListView.Items.Add(new
                {
                    item.Id,
                    item.TourId,
                    item.NumberOfGuests,
                }); 
            }
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;

            var newReservation = new TourReservation
            {
                Id = tourReservationService.GenerateId(),
                NumberOfGuests = guestCount,
                GuestId = 1,
                TourId = tourId,
            };

            selectedTour.GuestNumber += guestCount;
            tourReservationService.TourReservating(newReservation);
            MessageBox.Show("Reservation successful");

            ReservationsListView.Items.Add(newReservation);
            ClearInput();
        }

        private bool ValidateInput()
        {
            if (!ValidateAccommodationSelection()) return false;
            if (!ValidateGuestCount()) return false;

            return true;
        }

        private bool ValidateAccommodationSelection()
        {
            if (TourDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a tour you would like to reserve.");
                return false;
            }

            selectedTour = (Tour)TourDataGrid.SelectedItem;
            tourId = selectedTour.Id;
            return true;
        }

        private bool ValidateGuestCount()
        {
            if (!int.TryParse(GuestCountTextBox.Text, out guestCount) || guestCount < 1)
            {
                MessageBox.Show("Please enter a valid guest count.");
                return false;
            }

            if (selectedTour.MaxNumberOfGuests == selectedTour.GuestNumber)
            {
                string selectedCity = selectedTour.Location.City;
                TourDataGrid.Items.Clear();
                foreach (Tour item in tourService.GetAll())
                {
                    if (item.Location.City.Equals(selectedTour.Location.City) && item.Id != selectedTour.Id)
                    {
                        TourDataGrid.Items.Add(item);
                    }
                }
                MessageBox.Show("Unfortunately there are no more places left on that tour.\n You can check out tours similar to that one!");
                return false;
            }

            if (guestCount > (selectedTour.MaxNumberOfGuests - selectedTour.GuestNumber))
            {
                MessageBox.Show("There are not that many places left on this tour!\n Please lower the amount of people.");
                return false;
            }
            return true;
        }

        private void ClearInput()
        {
            GuestCountTextBox.Text = string.Empty;
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            TourDataGrid.Items.Clear();
            foreach (Tour item in tourService.GetAll())
            {
                TourDataGrid.Items.Add(item);
            }
        }
    }
}
