using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristHomeNotificationModel : ViewModelBase
    {
        private RelayCommand gotItCommand;
        private string tourName;
        private string city;
        private string country;
        private string description;
        private string language;

        public TouristHomeNotificationModel(Tour tour)
        {
            TourName = tour.Name;
            City = tour.Location.City;
            Country = tour.Location.Country;
            Description = tour.Description;
            Language = tour.Language.ToString();

        }

        private bool CanThisCommandExecute()
        {
            string currentUri = TouristMainWindow.navigationService?.CurrentSource?.ToString();
            return string.IsNullOrEmpty(currentUri);
        }
        private void GotItCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristHomeView.xaml", UriKind.Relative));
        }

        public RelayCommand GotItCommand
        {
            get
            {
                if (gotItCommand == null)
                {
                    gotItCommand = new RelayCommand(param => GotItCommandExecute(), param => CanThisCommandExecute());
                }

                return gotItCommand;
            }
        }

        public string TourName
        {
            get { return tourName; }
            set
            {
                tourName = value;
                OnPropertyChanged(nameof(TourName));
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Language
        {
            get { return language; }
            set
            {
                language = value;
                OnPropertyChanged(nameof(Language));
            }
        }


    }
}
