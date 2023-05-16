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
    internal class TourGuideLanguageCreateTourPageModel : ViewModelBase
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


        private int id;
        private string name;
        private string country;
        private string city;
        private string startingDate;
        private string time;
        private string maxNumberOfGuests;
        private string duration;
        private string description;
        private string keyPointId;
        private string keyPointName;
        private List<KeyPoints> keyPoints;

        private Language lang;

        private Tour selectedTour;

        private ObservableCollection<Tour> tours = new ObservableCollection<Tour>();

        private TourService tourService;
        private KeyPointsService keyPointsService;
        private TourReservationService tourReservationService;
        private VoucherService voucherService;
        private UserService userService;
        private TourRequestService tourRequestService;

        private RelayCommand createTour;


        public TourGuideLanguageCreateTourPageModel()
        {
            SetService();
            var tours = tourRequestService.GetAll();
            int english =   0;
            int serbian =   0;
            int spanish =   0;
            int norwegian = 0;
            foreach(TourRequest tour in tours)
            {
                if(tour.Language == "ENGLISH")
                {
                    english++;
                }

                if (tour.Language == "SERBIAN")
                {
                    serbian++;
                }

                if (tour.Language == "SPANISH")
                {
                    spanish++;
                }

                if (tour.Language == "NORWEGIAN")
                {
                    norwegian++;
                }
            }

            if(english >= serbian && english >= spanish && english >= norwegian)
            {
                Lang = Language.ENGLISH;
            }

            if (serbian >= english && serbian >= spanish && serbian >= norwegian)
            {
                Lang = Language.SERBIAN;
            }

            if (spanish >= english && spanish >= serbian && spanish >= norwegian)
            {
                Lang = Language.SPANISH;
            }

            if (norwegian >= english && norwegian >= serbian && norwegian >= spanish)
            {
                Lang = Language.NORWEGIAN;
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

        private void CreateTourExecute()
        {
            #region create tour
            TourService tourService = new TourService();
            KeyPointsService keyPointsService = new KeyPointsService();
            Tour newTour = new Tour();
            newTour.Id = 15;
            newTour.Name = Name;
            newTour.Location.Country = Country;
            newTour.Location.City = City;
            newTour.Language = Lang;
            newTour.StartingDate = DateTime.Parse(StartingDate);
            newTour.StartingTime = Time;
            newTour.MaxNumberOfGuests = int.Parse(MaximumNumberOfGuests);
            newTour.Duration = int.Parse(Duration);
            newTour.Description = Description;
            newTour.GuestNumber = 22;
            newTour.AssociatedTourGuide = 0;

            //////
            string i = KeyPointId;
            string j = KeyPointName;
            string[] keyId = i.Split(' ');
            string[] keyName = j.Split(' ');
            int k = 0;




            foreach (var word in keyId)
            {


                if (word == "x")
                {
                    break;
                }
                KeyPoints key = new KeyPoints();
                key.Id = int.Parse(word);
                key.Name = keyName[k];
                key.IsActive = false;
                key.AssociatedTour = 15;
                newTour.KeyPoints.Add(key);
                keyPointsService.Add(key);
                k++;
            }



            //////
            ///
            tourService.Add(newTour);
            #endregion
            TourGuideMainWindow.navigationService.Navigate(
               new Uri("UI/Dialogs/View/TourGuideView/TourGuideAllToursPageView.xaml", UriKind.Relative));
        }

        public RelayCommand CreateTour
        {
            get
            {
                if (createTour == null)
                {
                    createTour = new RelayCommand(param => CreateTourExecute());
                }

                return createTour;
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

        public Language Lang
        {
            get { return lang; }
            set
            {
                lang = value;
                OnPropertyChanged(nameof(Lang));

            }
        }
        public int Id
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


        public string StartingDate
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
    }
}
