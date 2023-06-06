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
using System.Windows;

namespace projekatSIMS.UI.Dialogs.ViewModel.TourGuideViewModel
{
    internal class TourGuideComplexTourPageModel : ViewModelBase
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

        private string date;
        private string fLanguage;
        private string fDate;
        private string fNumber;
        private string fLocation;

        private Tour selectedTour;
        private Language selectedLanguage;

        private ObservableCollection<Tour> tours = new ObservableCollection<Tour>();
        private ObservableCollection<TourRequest> toursR = new ObservableCollection<TourRequest>();

        private TourService tourService;
        private KeyPointsService keyPointsService;
        private TourReservationService tourReservationService;
        private VoucherService voucherService;
        private UserService userService;
        private TourRequestService tourRequestService;
        private ComplexTourRequestService complexTourRequestService;

        private RelayCommand tourButtonCommand;
        

        private RelayCommand fLanguageCommand;
        private RelayCommand fDateCommand;
        private RelayCommand fNumberCommand;
        private RelayCommand fLocationCommand;



        public TourGuideComplexTourPageModel()
        {
            SetService();
            var tours = tourService.GetAll();
            var tourr = tourRequestService.GetAll();
            var ture = complexTourRequestService.GetAll();

            int i = 15;





            foreach (TourRequest tour in tourr)
            {

                foreach (ComplexTourRequest toure in ture)
                {
                    if (tour.Id == 6 || tour.Id == 7)
                    { 
                    Tour tour1 = new Tour();
                    if (selectedLanguage == Language.ENGLISH)
                    {
                        tour1.Id = i;
                        tour1.AssociatedTourGuide = 0;
                        tour1.Duration = 2;
                        tour1.GuestNumber = 0;
                        tour1.MaxNumberOfGuests = tour.GuestNumber;
                        tour1.Description = tour.Description;
                        if (tour.Language == "ENGLISH")
                        {
                            tour1.Language = Language.ENGLISH;
                        }
                        if (tour.Language == "SERBIAN")
                        {
                            tour1.Language = Language.SERBIAN;
                        }
                        if (tour.Language == "SPANISH")
                        {
                            tour1.Language = Language.SPANISH;
                        }
                        if (tour.Language == "NORWEGIAN")
                        {
                            tour1.Language = Language.NORWEGIAN;
                        }
                        tour1.Location = tour.Location;
                        tour1.Name = "Tura" + i.ToString();
                        Date = tour.StartDate.Date.ToString("dd.MM.yyyy") + "--" + tour.EndDate.Date.ToString("dd.MM.yyyy");
                        Tours.Add(tour1);
                        i++;
                    }
                }
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
            complexTourRequestService = new ComplexTourRequestService();
        }

        private void TourButtonCommandExecute()
        {
            var tours = tourRequestService.GetAll();
            if (selectedTour != null)
            {
                foreach (TourRequest tour in tours)
                {
                    if (tour.Description == selectedTour.Description)
                    {
                        tour.Date = FDate;
                        tour.Status = TourRequestStatus.ACCEPTED;
                        tourRequestService.Edit(tour);
                    }
                }
            }
            else
            {
                MessageBox.Show("eeeeeeeeeeeee");
            }
        }

        private void FLanguageCommandExecute()
        {
            /*
             Tours.Clear();
             var tours = tourService.GetAll();
             var tourr = tourRequestService.GetAll();

             int i = 15;
             foreach (TourRequest tour in tourr)
             {
                 Tour tour1 = new Tour();
                 if (tour.Id <4)
                 {
                     tour1.Id = i;
                     tour1.AssociatedTourGuide = 0;
                     tour1.Duration = 2;
                     tour1.GuestNumber = 0;
                     tour1.MaxNumberOfGuests = tour.GuestNumber;
                     tour1.Description = tour.Description;
                     if (tour.Language == "ENGLISH")
                     {
                         tour1.Language = Language.ENGLISH;
                     }
                     if (tour.Language == "SERBIAN")
                     {
                         tour1.Language = Language.SERBIAN;
                     }
                     if (tour.Language == "SPANISH")
                     {
                         tour1.Language = Language.SPANISH;
                     }
                     if (tour.Language == "NORWEGIAN")
                     {
                         tour1.Language = Language.NORWEGIAN;
                     }
                     tour1.Location = tour.Location;
                     tour1.Name = "Tura" + i.ToString();
                     Date = tour.StartDate.Date.ToString("dd.MM.yyyy") + "--" + tour.EndDate.Date.ToString("dd.MM.yyyy");
                     Tours.Add(tour1);
                     i++;
                 }




             }
            */

        }
        private void FDateCommandExecute()
        {

        }
        private void FNumberCommandExecute()
        {

        }
        private void FLocationCommandExecute()
        {

        }

        

        public RelayCommand FDateCommand
        {
            get
            {
                if (fDateCommand == null)
                {
                    fDateCommand = new RelayCommand(param => FDateCommandExecute());
                }

                return fDateCommand;
            }
        }

        public RelayCommand FLanguageCommand
        {
            get
            {
                if (fLanguageCommand == null)
                {
                    fLanguageCommand = new RelayCommand(param => FLanguageCommandExecute());
                }

                return fLanguageCommand;
            }
        }

        public RelayCommand FNumberCommand
        {
            get
            {
                if (fNumberCommand == null)
                {
                    fNumberCommand = new RelayCommand(param => FNumberCommandExecute());
                }

                return fNumberCommand;
            }
        }

        public RelayCommand FLocationCommand
        {
            get
            {
                if (fLocationCommand == null)
                {
                    fLocationCommand = new RelayCommand(param => FLocationCommandExecute());
                }

                return fLocationCommand;
            }
        }

        public RelayCommand TourButtonCommand
        {
            get
            {
                if (tourButtonCommand == null)
                {
                    tourButtonCommand = new RelayCommand(param => TourButtonCommandExecute());
                }

                return tourButtonCommand;
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

        public ObservableCollection<TourRequest> ToursR
        {
            get { return toursR; }
            set
            {
                toursR = value;
                OnPropertyChanged(nameof(ToursR));
            }
        }

        public Language SelectedLanguage
        {
            get { return selectedLanguage; }
            set
            {
                selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));

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
        public string FLanguage
        {
            get { return fLanguage; }
            set
            {
                fLanguage = value;
                OnPropertyChanged(nameof(FLanguage));

            }
        }
        public string FDate
        {
            get { return fDate; }
            set
            {
                fDate = value;
                OnPropertyChanged(nameof(FDate));

            }
        }
        public string FNumber
        {
            get { return fNumber; }
            set
            {
                fNumber = value;
                OnPropertyChanged(nameof(FNumber));

            }
        }
        public string FLocation
        {
            get { return fLocation; }
            set
            {
                fLocation = value;
                OnPropertyChanged(nameof(FLocation));

            }
        }

        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));

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
    }
}
