using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristVouchersModel : ViewModelBase
    {
        private RelayCommand useCommand;

        private ObservableCollection<Voucher> vouchers = new ObservableCollection<Voucher>();

        private VoucherService voucherService;
        private UserService userService;
        private TourReservationService reservationService;
        private string currentReservations;
        private string reservationsNeeded;
        public TouristVouchersModel()
        {
            SetService();
            CheckVoucherExpirationDate();
            LoadVouchers();
            SetCurrentReservations();
            SetNeededReservations();
        }

        private bool CanThisCommandExecute()
        {
            string currentUri = TouristMainWindow.navigationService?.CurrentSource?.ToString();
            return currentUri?.EndsWith("TouristVouchersView.xaml", StringComparison.OrdinalIgnoreCase) == true;
        }

        private void UseCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristSearchTourView.xaml", UriKind.Relative));
        }

        public void SetService()
        {
            voucherService = new VoucherService();
            userService = new UserService();
            reservationService = new TourReservationService();
        }

        public void CheckVoucherExpirationDate()
        {
            foreach (Voucher voucher in voucherService.GetAll().ToList())
            {
                if (voucher.ExpirationDate.Date <= DateTime.Today.Date)
                {
                    voucherService.Remove(voucher);
                }
            }
        }
        public void SetCurrentReservations()
        {
            int i = 0;
            foreach(TourReservation tourReservation in reservationService.GetAll().ToList())
            {
                if (tourReservation.GuestId == userService.GetLoginUser().Id){ i++; }
            }
            currentReservations = i.ToString();
        }

        public void SetNeededReservations()
        {
            int i = 0;
            foreach (TourReservation tourReservation in reservationService.GetAll().ToList())
            {
                if (tourReservation.GuestId == userService.GetLoginUser().Id) { i++; }
            }
            reservationsNeeded = (5-i).ToString(); 
        }
        public void LoadVouchers()
        {
            foreach (Voucher voucher in voucherService.GetAll())
            {
                if (voucher.GuestId == userService.GetLoginUser().Id)
                {
                    vouchers.Add(voucher);
                }
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

        public RelayCommand UseCommand
        {
            get
            {
                return useCommand ?? (useCommand = new RelayCommand(param => UseCommandExecute(), param => CanThisCommandExecute()));
            }
        }

        public string CurrentReservations
        {
            get { return  currentReservations; }
            set {
                currentReservations = value;
                OnPropertyChanged(nameof(CurrentReservations));
            }
        }

        public string NeededReservations
        {
            get { return reservationsNeeded; }
            set
            {
                reservationsNeeded = value;
                OnPropertyChanged(nameof(NeededReservations));
            }
        }
    }
}
