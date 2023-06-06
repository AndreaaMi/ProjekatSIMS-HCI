using projekatSIMS.CompositeComon;
using projekatSIMS.UI.Dialogs.View.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace projekatSIMS.UI.Dialogs.ViewModel.GuestViewModel
{

    public class GuestMainViewModel : ViewModelBase
    {
        ActiveReservationsViewModel activeReservationsViewModel = new ActiveReservationsViewModel();
        ActiveReservationsView activeReservationsView = new ActiveReservationsView();
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

        private ObservableCollection<UserControl> _demoSteps = new ObservableCollection<UserControl>();
        public ObservableCollection<UserControl> DemoSteps
        {
            get { return _demoSteps; }
            set
            {
                _demoSteps = value;
                OnPropertyChanged(nameof(DemoSteps));
            }
        }

        public ICommand ShowNewUserControlCommand { get; private set; }
        public ICommand ShowGuestPageRequestCommand { get; private set; }
        public ICommand ShowRescheduleReservationCommand { get; private set; }
        public ICommand ShowActiveReservationCommand { get; private set; }
        public ICommand ShowAnywhereAnytimeViewCommand { get; private set; }
        public ICommand ShowAllAccommodationsCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }
        public ICommand PlayDemoCommand { get; private set; }
        public ICommand ShowGuestPageBonusCommand { get; private set; }

        public ICommand ShowForumCommand { get; private set; }  

        public GuestMainViewModel()
        {
            ShowAnywhereAnytimeViewCommand = new RelayCommand(ShowAnywhereAnytimeViewControl);
            ShowAllAccommodationsCommand = new RelayCommand(ShowAllAccommodationsControl);
            ShowNewUserControlCommand = new RelayCommand(ShowNewUserControl);
            ShowGuestPageRequestCommand = new RelayCommand(ShowGuestPageRequestControl);
            LogoutCommand = new RelayCommand(Logout);
            ShowActiveReservationCommand = new RelayCommand(ShowActiveReservation);
            ShowForumCommand = new RelayCommand(ShowForumControl);
            ShowGuestPageBonusCommand = new RelayCommand(ShowGuestPageBonusControl);
            ShowNewUserControl(null);
            DemoSteps.Add(new GuestPageView());
            DemoSteps.Add(new ActiveReservationsView());
            DemoSteps.Add(new AnywhereAnytimeView());
            DemoSteps.Add(new AllAccommodationsView());

            SelectedView = DemoSteps.FirstOrDefault();
            PlayDemoCommand = new RelayCommand(PlayDemo);
        }

        private bool _isDemoPlaying;
        public bool IsDemoPlaying
        {
            get { return _isDemoPlaying; }
            set
            {
                _isDemoPlaying = value;
                OnPropertyChanged(nameof(IsDemoPlaying));
            }
        }
        private bool _demoMode;
        public bool DemoMode
        {
            get { return _demoMode; }
            set
            {
                _demoMode = value;
                OnPropertyChanged(nameof(DemoMode));
            }
        }
        private async void PlayDemo(object parameter)
        {
            if (IsDemoPlaying)
            {
                IsDemoPlaying = false; // Isključivanje demo moda
            }
            else
            {
                IsDemoPlaying = true; // Uključivanje demo moda

                foreach (var step in DemoSteps)
                {
                    ExecuteDemoStep(step);
                    SelectedView = step;
                    await Task.Delay(5000); // Pauza između svakog koraka, prilagodite prema potrebama

                    if (!IsDemoPlaying) // Ako je demo mod isključen, prekini izvršavanje demo koraka
                        break;
                }

                IsDemoPlaying = false; // Isključivanje demo moda
            }
        }
        private void ExecuteDemoStep(UserControl step)
        {
            // Ovdje možete dodati logiku za prikazivanje glavne funkcionalnosti svakog koraka
            if (step is ActiveReservationsView activeReservationsView)
            {
                activeReservationsViewModel.ExecuteDemoStep();
            }
            else if (step is AnywhereAnytimeView)
            {
                // Logika za AnywhereAnytimeView korak
            }
            else if (step is AllAccommodationsView)
            {
                // Logika za AllAccommodationsView korak
            }
            // Dodajte slične provjere za ostale korake
        }
        private void ShowAnywhereAnytimeViewControl(object parameter)
        {
            SelectedView = new AnywhereAnytimeView();
        }

        private void ShowNewUserControl(object parameter)
        {
            SelectedView = new GuestPageView();
        }

        private void ShowActiveReservation(object parameter)
        {
            SelectedView = new ActiveReservationsView();
        }

        private void ShowForumControl(object parameter)
        {
            SelectedView = new ForumView();
        }
        private void ShowGuestPageBonusControl(object parameter)
        {
            SelectedView = new GuestPageBonusView();
        }

        private void ShowAllAccommodationsControl(object parameter)
        {
            SelectedView = new AllAccommodationsView();
        }

        private void ShowGuestPageRequestControl(object parameter)
        {
            SelectedView = new GuestPageRequestView();
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
