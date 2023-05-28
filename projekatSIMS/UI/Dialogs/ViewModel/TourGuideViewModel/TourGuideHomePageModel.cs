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
    internal class TourGuideHomePageModel : ViewModelBase
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

        private Tour selectedTour;

        private ObservableCollection<Tour> tours = new ObservableCollection<Tour>();

        private TourService tourService;
        private KeyPointsService keyPointsService;
        private TourReservationService tourReservationService;
        private VoucherService voucherService;
        private UserService userService;
        private TourRatingService tourRatingService;

        private RelayCommand myToursCommand;
        private RelayCommand languageCommand;
        private RelayCommand locationCommand;
        private RelayCommand statsCommand;
        private RelayCommand quitCommand;


        public TourGuideHomePageModel()
        {
            SetService();
            var tours = tourService.GetAll();
            var tourR = tourRatingService.GetAll();
            var users = userService.GetAll();
            Rating = 0;
            int englishL = 0;
            int spanishL = 0;
            int serbianL = 0;
            int norwegianL = 0;


            foreach (Tour tour in tours)
            {

                if(tour.GuestNumber > Rating)
                {
                    Rating = tour.GuestNumber;
                }
               


                // Name = tour.Name;
                // Description = tour.Description;

            }
            foreach (Tour tour in tours)
            {
                foreach(TourRating tourr in tourR)
                {
                    if (tour.Language == Language.ENGLISH && tourr.TourId == tour.Id && tourr.InterestLevel > 4.5 && tour.StartingDate > DateTime.Now.AddYears(-1))
                    {
                        englishL++;
                    }
                    if (tour.Language == Language.SPANISH && tourr.TourId == tour.Id && tourr.InterestLevel > 4.5 && tour.StartingDate > DateTime.Now.AddYears(-1))
                    {
                        spanishL++;
                    }
                    if (tour.Language == Language.SERBIAN && tourr.TourId == tour.Id && tourr.InterestLevel > 4.5 && tour.StartingDate > DateTime.Now.AddYears(-1))
                    {
                        serbianL++;
                    }
                    if (tour.Language == Language.NORWEGIAN && tourr.TourId == tour.Id && tourr.InterestLevel > 4.5 && tour.StartingDate > DateTime.Now.AddYears(-1))
                    {
                        norwegianL++;
                    }
                }
            }
            if(englishL>4 || spanishL > 4 || serbianL > 4 || norwegianL > 4)
            {
                foreach(User user in users)
                {
                    if (user.Id == 4)
                    {
                        user.SuperStatus = true;
                        userService.Edit(user);
                        
                    }
                }    
            }
           

            if(norwegianL < 5 && englishL < 5 && serbianL < 5 && spanishL < 5)
            {
                foreach (User user in users)
                {
                    if (user.Id == 4)
                    {
                        user.SuperStatus = false;
                        userService.Edit(user);
                    }
                }
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
            tourRatingService = new TourRatingService();
        }

        private void StatsCommandExecute()
        {
            TourGuideMainWindow.navigationService.Navigate(
               new Uri("UI/Dialogs/View/TourGuideView/TourGuideTourStatsPageView.xaml", UriKind.Relative));
        }

        private void MyToursCommandExecute()
        {
            TourGuideMainWindow.navigationService.Navigate(
               new Uri("UI/Dialogs/View/TourGuideView/TourGuideMyToursPageView.xaml", UriKind.Relative));
        }

        private void LanguageCommandExecute()
        {
            TourGuideMainWindow.navigationService.Navigate(
               new Uri("UI/Dialogs/View/TourGuideView/TourGuideLanguageCreateTour.xaml", UriKind.Relative));
        }

        private void LocationCommandExecute()
        {
            TourGuideMainWindow.navigationService.Navigate(
               new Uri("UI/Dialogs/View/TourGuideView/TourGuideLocationCreateTour.xaml", UriKind.Relative));
        }

        private void QuitCommandExecute()
        {
            var tours = tourService.GetAll();
            var tourR = tourReservationService.GetAll();
            List<Tour> toursToRemove = new List<Tour>();
            List<TourReservation> tourrToRemove = new List<TourReservation>();
            foreach (Tour tour in tours)
            {
                if(tour.StartingDate > DateTime.Now)
                {
                    
                    foreach(TourReservation tourr in tourR)
                    {
                        if(tourr.TourId == tour.Id)
                        {
                            Voucher v = new Voucher();
                            v.ExpirationDate = DateTime.Now.AddYears(2);
                            v.GuestId = tourr.GuestId;
                            v.Id = 2;
                            voucherService.Add(v);
                            tourrToRemove.Add(tourr);
                        }
                    }
                    toursToRemove.Add(tour);
                }
            }
            foreach (Tour tourToRemove in toursToRemove)
            {
                tourService.Remove(tourToRemove);
            }
            foreach (TourReservation tourToRemove in tourrToRemove)
            {
                tourReservationService.Remove(tourToRemove);
            }
        }

        public RelayCommand QuitCommand
        {
            get
            {
                if (quitCommand == null)
                {
                    quitCommand = new RelayCommand(param => QuitCommandExecute());
                }

                return quitCommand;
            }
        }

        public RelayCommand StatsCommand
        {
            get
            {
                if (statsCommand == null)
                {
                    statsCommand = new RelayCommand(param => StatsCommandExecute());
                }

                return statsCommand;
            }
        }

        public RelayCommand LocationCommand
        {
            get
            {
                if (locationCommand == null)
                {
                    locationCommand = new RelayCommand(param => LocationCommandExecute());
                }

                return locationCommand;
            }
        }

        public RelayCommand LanguageCommand
        {
            get
            {
                if (languageCommand == null)
                {
                    languageCommand = new RelayCommand(param => LanguageCommandExecute());
                }

                return languageCommand;
            }
        }

        public RelayCommand MyToursCommand
        {
            get
            {
                if (myToursCommand == null)
                {
                    myToursCommand = new RelayCommand(param => MyToursCommandExecute());
                }

                return myToursCommand;
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
