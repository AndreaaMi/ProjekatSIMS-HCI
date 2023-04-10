using projekatSIMS.CompositeComon;
using projekatSIMS.UI.Dialogs.View.GuestView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace projekatSIMS.UI.Dialogs.ViewModel.GuestViewModel
{
    public class GuestMainViewModel : ViewModelBase
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

        public ICommand ShowNewUserControlCommand { get; private set; }
        public ICommand ShowRescheduleReservationCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }

        public GuestMainViewModel()
        {
            ShowNewUserControlCommand = new RelayCommand(ShowNewUserControl);
            ShowRescheduleReservationCommand = new RelayCommand(ShowRescheduleReservation);
            LogoutCommand = new RelayCommand(Logout);
        }

        private void ShowNewUserControl(object parameter)
        {
            SelectedView = new GuestPageView();
        }

        private void ShowRescheduleReservation(object parameter)
        {
            SelectedView = new RescheduleReservationNotificationView();
        }

        private void Logout(object parameter)
        {
            // Navigate to the main window, which is your login page
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Close the current window
            var currentWindow = parameter as Window;
            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }
    }
}
