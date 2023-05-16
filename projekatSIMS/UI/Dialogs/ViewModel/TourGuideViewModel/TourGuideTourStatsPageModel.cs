using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.TourGuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.TourGuideViewModel
{
    internal class TourGuideTourStatsPageModel : ViewModelBase
    {
        #region SIDE BAR
        private RelayCommand profilePageCommand;
        private RelayCommand homePageCommand;
        private RelayCommand newTourPageCommand;
        private RelayCommand allToursPageCommand;
        private RelayCommand toursTodayPageCommand;
        private RelayCommand tourRequestsPageCommand;

        private void ProfilePageCommandExecute()
        {

            TourGuideMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TourGuideView/TourGuideProfilePageView.xaml", UriKind.Relative));


        }

        private void HomePageCommandExecute()
        {

            TourGuideMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TourGuideView/TourGuideHomePageView.xaml", UriKind.Relative));


        }

        private void NewTourPageCommandExecute()
        {

            TourGuideMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TourGuideView/TourGuideNewTourPageView.xaml", UriKind.Relative));


        }

        private void AllToursPageCommandExecute()
        {

            TourGuideMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TourGuideView/TourGuideAllToursPageView.xaml", UriKind.Relative));


        }

        private void ToursTodayPageCommandExecute()
        {

            TourGuideMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TourGuideView/TourGuideToursTodayPageView.xaml", UriKind.Relative));


        }

        private void TourRequestsPageCommandExecute()
        {

            TourGuideMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TourGuideView/TourGuideTourRequestsPageView.xaml", UriKind.Relative));


        }
        public RelayCommand ProfilePageCommand
        {
            get
            {
                if (profilePageCommand == null)
                {
                    profilePageCommand = new RelayCommand(param => ProfilePageCommandExecute());
                }

                return profilePageCommand;
            }
        }

        public RelayCommand HomePageCommand
        {
            get
            {
                if (homePageCommand == null)
                {
                    homePageCommand = new RelayCommand(param => HomePageCommandExecute());
                }

                return homePageCommand;
            }
        }

        public RelayCommand NewTourPageCommand
        {
            get
            {
                if (newTourPageCommand == null)
                {
                    newTourPageCommand = new RelayCommand(param => NewTourPageCommandExecute());
                }

                return newTourPageCommand;
            }
        }

        public RelayCommand AllToursPageCommand
        {
            get
            {
                if (allToursPageCommand == null)
                {
                    allToursPageCommand = new RelayCommand(param => AllToursPageCommandExecute());
                }

                return allToursPageCommand;
            }
        }

        public RelayCommand ToursTodayPageCommand
        {
            get
            {
                if (toursTodayPageCommand == null)
                {
                    toursTodayPageCommand = new RelayCommand(param => ToursTodayPageCommandExecute());
                }

                return toursTodayPageCommand;
            }
        }

        public RelayCommand TourRequestsPageCommand
        {
            get
            {
                if (tourRequestsPageCommand == null)
                {
                    tourRequestsPageCommand = new RelayCommand(param => TourRequestsPageCommandExecute());
                }

                return tourRequestsPageCommand;
            }
        }


        #endregion


        private string id;
        private string name;
        private string country;
        private string city;
        private DateTime startingDate;
        private string time;
        private string maxNumberOfGuests;
        private string duration;
        private string description;
        private string keyPointId;
        private string keyPointName;
        private List<KeyPoints> keyPoints;

        private double rating;
        private int text;

        private Tour selectedTour;

        private ObservableCollection<Tour> tours = new ObservableCollection<Tour>();

        private TourService tourService;
        private KeyPointsService keyPointsService;
        private TourReservationService tourReservationService;
        private VoucherService voucherService;
        private UserService userService;
        private TourRequestService tourRequestService;

        private RelayCommand serbianCommand;
        private RelayCommand spanishCommand;
        private RelayCommand englishCommand;
        private RelayCommand norwegianCommand;
        private RelayCommand osloCommand;
        private RelayCommand madridCommand;
        private RelayCommand barcelonaCommand;
        private RelayCommand alesundCommand;


        public TourGuideTourStatsPageModel()
        {
            SetService();
            var tours = tourService.GetAll();
            Rating = 0;



            foreach (Tour tour in tours)
            {

                if (tour.GuestNumber > Rating)
                {
                    Rating = tour.GuestNumber;
                }


                // Name = tour.Name;
                // Description = tour.Description;

            }
            foreach (Tour tour in tours)
            {

                if (tour.GuestNumber == Rating)
                {
                    Name = tour.Name;
                    MaximumNumberOfGuests = tour.GuestNumber.ToString();
                    break;
                }


                // Name = tour.Name;
                // Description = tour.Description;

            }





        }

        public void SetService()
        {
            tourService = new TourService();
            keyPointsService = new KeyPointsService();
            tourReservationService = new TourReservationService();
            voucherService = new VoucherService();
            userService = new UserService();
            tourRequestService = new TourRequestService();
        }

        private void SerbianCommandExecute()
        {
            var tours = tourRequestService.GetAll();
            int i = 0;
            foreach (TourRequest tour in tours)
            {
                if(tour.Language == "SERBIAN")
                { i++; }
                Text = i;

            }
        }

        private void SpanishCommandExecute()
        {
            var tours = tourRequestService.GetAll();
            int i = 0;
            foreach (TourRequest tour in tours)
            {
                if (tour.Language == "SPANISH")
                { i++; }
            }
            Text = i;

        }

        private void EnglishCommandExecute()
        {
            var tours = tourRequestService.GetAll();
            int i = 0;
            foreach (TourRequest tour in tours)
            {
                if (tour.Language == "ENGLISH")
                { i++; }
            }
            Text = i;

        }

        private void NorwegianCommandExecute()
        {
            var tours = tourRequestService.GetAll();
            int i = 0;
            foreach (TourRequest tour in tours)
            {
                if (tour.Language == "NORWEGIAN")
                { i++; }
            }
            Text = i;

        }

        private void OsloCommandExecute()
        {
            var tours = tourRequestService.GetAll();
            int i = 0;
            foreach (TourRequest tour in tours)
            {
                if (tour.Location.city == "Oslo")
                { i++; }
            }
            Text = i;

        }

        private void MadridCommandExecute()
        {
            var tours = tourRequestService.GetAll();
            int i = 0;
            foreach (TourRequest tour in tours)
            {
                if (tour.Location.city == "Madrid")
                { i++; }
            }
            Text = i;

        }

        private void BarcelonaCommandExecute()
        {
            var tours = tourRequestService.GetAll();
            int i = 0;
            foreach (TourRequest tour in tours)
            {
                if (tour.Location.city == "Barcelona")
                { i++; }
            }
            Text = i;
        }

        private void AlesundCommandExecute()
        {
            var tours = tourRequestService.GetAll();
            int i = 0;
            foreach (TourRequest tour in tours)
            {
                if (tour.Location.city == "Alesund")
                { i++; }
            }
            Text = i;
        }

        public RelayCommand OsloCommand
        {
            get
            {
                if (osloCommand == null)
                {
                    osloCommand = new RelayCommand(param => OsloCommandExecute());
                }

                return osloCommand;
            }
        }
        public RelayCommand MadridCommand
        {
            get
            {
                if (madridCommand == null)
                {
                    madridCommand = new RelayCommand(param => MadridCommandExecute());
                }

                return madridCommand;
            }
        }

        public RelayCommand BarcelonaCommand
        {
            get
            {
                if (barcelonaCommand == null)
                {
                    barcelonaCommand = new RelayCommand(param => BarcelonaCommandExecute());
                }

                return barcelonaCommand;
            }

        }

        public RelayCommand AlesundCommand
        {
            get
            {
                if (alesundCommand == null)
                {
                    alesundCommand = new RelayCommand(param => AlesundCommandExecute());
                }

                return alesundCommand;
            }
        }


        public RelayCommand SerbianCommand
        {
            get
            {
                if (serbianCommand == null)
                {
                    serbianCommand = new RelayCommand(param => SerbianCommandExecute());
                }

                return serbianCommand;
            }
        }

        public RelayCommand SpanishCommand
        {
            get
            {
                if (spanishCommand == null)
                {
                    spanishCommand = new RelayCommand(param => SpanishCommandExecute());
                }

                return spanishCommand;
            }
                
        }

        public RelayCommand EnglishCommand
        {
            get
            {
                if (englishCommand == null)
                {
                    englishCommand = new RelayCommand(param => EnglishCommandExecute());
                }

                return englishCommand;
            }
        }

        public RelayCommand NorwegianCommand
        {
            get
            {
                if (norwegianCommand == null)
                {
                    norwegianCommand = new RelayCommand(param => NorwegianCommandExecute());
                }

                return norwegianCommand;
            }
        }

        public ObservableCollection<Tour> Tours
        {
            get { return tours; }
            set
            {
                tours = value;
                OnPropertyChanged(nameof(Tours));
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

        public int Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));

            }
        }
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));

            }
        }




        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
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

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }


        public DateTime StartingDate
        {
            get { return startingDate; }
            set
            {
                startingDate = value;
                OnPropertyChanged(nameof(StartingDate));
            }
        }


        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged(nameof(Time));
            }
        }


        public string MaximumNumberOfGuests
        {
            get { return maxNumberOfGuests; }
            set
            {
                maxNumberOfGuests = value;
                OnPropertyChanged(nameof(MaximumNumberOfGuests));
            }
        }


        public string Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged(nameof(Duration));
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


        public string KeyPointId
        {
            get { return keyPointId; }
            set
            {
                keyPointId = value;
                OnPropertyChanged(nameof(KeyPointId));
            }
        }


        public string KeyPointName
        {
            get { return keyPointName; }
            set
            {
                keyPointName = value;
                OnPropertyChanged(nameof(KeyPointName));
            }
        }

        public List<KeyPoints> KeyPoints
        {
            get { return keyPoints; }
            set
            {
                keyPoints = value;
                OnPropertyChanged(nameof(KeyPoints));
            }


        }

        public double Rating
        {
            get { return rating; }
            set
            {
                rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }
    }
}
