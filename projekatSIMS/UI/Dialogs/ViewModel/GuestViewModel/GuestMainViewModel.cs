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
using System.Windows.Navigation;

namespace projekatSIMS.UI.Dialogs.ViewModel.GuestViewModel
{

    public class GuestMainViewModel : ViewModelBase
    {
        public static NavigationService navigationService;

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

        public ICommand ShowActiveReservationCommand { get; private set; }

        public ICommand ShowAllAccommodationsCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }

        public GuestMainViewModel()
        {
            ShowAllAccommodationsCommand = new RelayCommand(ShowAllAccommodationsControl);
            ShowNewUserControlCommand = new RelayCommand(ShowNewUserControl);
            LogoutCommand = new RelayCommand(Logout);
            ShowActiveReservationCommand = new RelayCommand(ShowActiveReservation);

        }

        private void ShowNewUserControl(object parameter)
        {
            SelectedView = new GuestPageView();
        }

        private void ShowActiveReservation(object parameter)
        {
            SelectedView = new ActiveReservationsView();
        }

        private void ShowAllAccommodationsControl(object parameter)
        {
            SelectedView = new AllAccommodationsView();
        }
        private void Logout(object parameter)
        {
            // Close all open windows except the main window
            foreach (Window window in Application.Current.Windows)
            {
                if (window != Application.Current.MainWindow)
                {
                    window.Close();
                }
            }

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
