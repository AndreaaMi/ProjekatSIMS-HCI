using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristReservationModel : ViewModelBase
    {
        private RelayCommand backCommand;
        private RelayCommand confirmCommand;
        
        private ObservableCollection<Tour> items = new ObservableCollection<Tour>();
        private ObservableCollection<Voucher> vouchers = new ObservableCollection<Voucher>();

        private Tour selectedTour = new Tour();
        private Voucher selectedVoucher;
        private string guestNumber;

        private VoucherService voucherService;
        private UserService userService;
        private TourService tourService;
        private TourReservationService tourReservationService;

        public TouristReservationModel(Tour selectedTour)
        {
            SetService();
            Items.Add(selectedTour);
            CheckVoucherExpirationDate();
            LoadVouchers();
            
        }

        #region COMMANDS
        private void BackCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristSearchTourView.xaml", UriKind.Relative));
        }
        private void ConfirmCommandExecute()
        {
            UseVoucher();
            if (!CanConfirmCommandExecute()) return;
            MessageBoxResult result = MessageBox.Show("Your reservation is successful", "Thank you. We appreciate it!", MessageBoxButton.OK);
            if (result == MessageBoxResult.OK)
            {
                TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristHomeView.xaml", UriKind.Relative));
            }
        }

        private bool CanConfirmCommandExecute()
        {
            if (!ValidateGuestCount()) return false;
            if (!IsTourFull()) return false;
            if (!IsGuestCountValidForTour()) return false;
            CreateReservation();
            return true;
        }

        public void SetService()
        {
            voucherService = new VoucherService();
            userService = new UserService();
            tourService = new TourService();
            tourReservationService = new TourReservationService();
        }
        #endregion

        #region VOUCHER_LOGIC
        public void CheckVoucherExpirationDate()
        {
            foreach(Voucher voucher in voucherService.GetAll().ToList())
            {
                if (voucher.ExpirationDate.Date <= DateTime.Today.Date)
                {
                    voucherService.Remove(voucher);
                }
            }
        }
        public void LoadVouchers()
        {
            foreach(Voucher voucher in voucherService.GetAll())
            {
                if (voucher.GuestId == userService.GetLoginUser().Id)
                {
                    vouchers.Add(voucher);
                }
            }
        }

        public void UseVoucher()
        {
            if(selectedVoucher != null)
            {
                voucherService.Remove(SelectedVoucher);
            }
        }

        #endregion

        #region RESERVATION LOGIC
        private bool ValidateGuestCount()
        {
            if (!int.TryParse(GuestNumber, out int guestCount) || guestCount < 1)
            {
                MessageBox.Show("Please enter a valid guest count.");
                return false;
            }
            return true;
        }
        private bool IsTourFull()
        {
            if (SelectedTour.GuestNumber == SelectedTour.MaxNumberOfGuests)
            {
                string city = SelectedTour.Location.City;
                int id = SelectedTour.Id;
                Items.Clear();
                foreach (Tour tour in tourService.GetAll())
                {
                    if (tour.Location.City == city && tour.Id != id)
                    {
                        Items.Add(tour);
                    }
                }
                MessageBox.Show("Unfortunately there are no more places left on that tour.\n You can check out tours similar to that one!");
                return false;
            }
            return true;
        }

        private bool IsGuestCountValidForTour()
        {
            if (int.Parse(GuestNumber) > (SelectedTour.MaxNumberOfGuests - SelectedTour.GuestNumber))
            {
                MessageBox.Show("There are not that many places left on this tour!\n Please lower the amount of people.");
                return false;
            }
            return true;
        }

        //private bool IsTourSelected()
        //{
        //    if (SelectedTour == null) return false;
        //    else return true;
        //}

        public void CreateReservation()
        {
            SelectedTour.GuestNumber += int.Parse(GuestNumber);
            //CREATING A NEW TOUR RESERVATION AND SETTING THE VALUES
            TourReservation tourReservation = new TourReservation(tourReservationService.GenerateId(),userService.GetLoginUser().Id, SelectedTour.Id, int.Parse(GuestNumber));
            tourReservationService.Add(tourReservation);
        }
        #endregion

        #region PROPERTIES

        public ObservableCollection<Tour> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public Tour SelectedTour
        {
            get { return selectedTour; }
            set
            {
                selectedTour = value; 
                OnPropertyChanged(nameof(SelectedTour));
            }
        }

        public RelayCommand BackCommand
        {
            get
            {
                return backCommand ?? (backCommand = new RelayCommand(param => BackCommandExecute()));
            }
        }

        public string GuestNumber
        {
            get { return guestNumber; }
            set
            {
                guestNumber = value;
                OnPropertyChanged(nameof(GuestNumber));
            }
        }

        public ObservableCollection<Voucher> Vouchers
        {
            get { return vouchers; }
            set
            {
                vouchers = value;
                OnPropertyChanged(nameof(Vouchers));
            }
        }

        public Voucher SelectedVoucher
        {
            get { return selectedVoucher; }
            set
            {
                selectedVoucher = value;
                OnPropertyChanged(nameof(SelectedVoucher));
            }
        }

        public RelayCommand ConfirmCommand
        {
            get
            {
                return confirmCommand ?? (confirmCommand = new RelayCommand(param => ConfirmCommandExecute()));
            }
        }

        #endregion
    }
}
