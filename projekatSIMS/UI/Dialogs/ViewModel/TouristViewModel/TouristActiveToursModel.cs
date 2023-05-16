using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristActiveToursModel : ViewModelBase
    {
        private RelayCommand rateCommand;

        private ObservableCollection<Tour> items = new ObservableCollection<Tour>();
        private ObservableCollection<KeyPoints> keypoints = new ObservableCollection<KeyPoints>();  

        private TourService tourService;
        private KeyPointsService keyPointsService;
        private TourReservationService tourReservationService;
        private UserService userService;

        public TouristActiveToursModel()
        {
            SetService();
            LoadActiveTours();
        }

        public void LoadActiveTours()
        {
            List<int> reservatedTours = tourReservationService.GetReservationByGuestId(userService.GetLoginUser().Id);
            foreach(int id in reservatedTours)
            {
                if (keyPointsService.HasTheTourStarted(id) && !keyPointsService.HasTheTourEnded(id))
                {
                    Tour tour = (Tour)tourService.Get(id);
                    items.Add(tour);
                    KeyPoints keypoint = (KeyPoints)keyPointsService.Get(keyPointsService.GetActiveKeyPointId(id));
                    keypoints.Add(keypoint);
                }
            }
        }
        public void SetService()
        {
            keyPointsService = new KeyPointsService();
            tourService = new TourService();
            tourReservationService = new TourReservationService();
            userService = new UserService();
        }
        private bool CanThisCommandExecute()
        {
            string currentUri = TouristMainWindow.navigationService?.CurrentSource?.ToString();
            return currentUri?.EndsWith("TouristActiveToursView.xaml", StringComparison.OrdinalIgnoreCase) == true;
        }
        private void RateCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristRatingToursView.xaml", UriKind.Relative));
        }
        public ObservableCollection<Tour> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public ObservableCollection<KeyPoints> Keypoints
        {
            get { return keypoints; }
            set
            {
                keypoints = value;
                OnPropertyChanged(nameof(Keypoints));
            }
        }

        public RelayCommand RateCommand
        {
            get
            {
                if (rateCommand == null)
                {
                    rateCommand = new RelayCommand(param => RateCommandExecute(), param => CanThisCommandExecute());
                }

                return rateCommand;
            }
        }
    }
}
