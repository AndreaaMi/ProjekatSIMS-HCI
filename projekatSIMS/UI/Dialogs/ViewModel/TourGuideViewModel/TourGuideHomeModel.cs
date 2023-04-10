using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.TourGuideView;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.TourGuideViewModel
{
    internal class TourGuideHomeModel : ViewModelBase
    {
        private RelayCommand createCommand;
        private RelayCommand nextCommand;

        private ObservableCollection<Tour> items = new ObservableCollection<Tour>();

        private string id;
        private string name;
        private string country;
        private string city;
        private string date;
        private string time;
        private string maxNumberOfGuests;
        private string duration;
        private string description;
        private string keyPointId;
        private string keyPointName;

        public TourGuideHomeModel()
        {

        }

        private void CreateCommandExecute()
        {
            TourService tourService = new TourService();
            KeyPointsService keyPointsService = new KeyPointsService();
            Tour newTour = new Tour();
            newTour.Id = int.Parse(Id);
            newTour.Name = Name;
            newTour.Location.Country = Country;
            newTour.Location.City = City;

            newTour.StartingDate = DateTime.Parse(Date);
            newTour.StartingTime = Time;
            newTour.MaxNumberOfGuests = int.Parse(MaximumNumberOfGuests);
            newTour.Duration = int.Parse(Duration);
            newTour.Description = Description;
            newTour.GuestNumber = 22;

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
                key.AssociatedTour = int.Parse(Id);
                newTour.KeyPoints.Add(key);
                keyPointsService.Add(key);
                k++;
            }



            //////
            ///
            tourService.Add(newTour);
            //dodavanje u datagrid
            Items.Add(newTour);
            
        }

        private void NextCommandExecute()
        {
            TourGuideMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristSearchTourView.xaml", UriKind.Relative));
        }




        public RelayCommand CreateCommand
        {
            get
            {
                if (createCommand == null)
                {
                    createCommand = new RelayCommand(param => CreateCommandExecute());
                }

                return createCommand;
            }
        }

        public RelayCommand NextCommand
        {
            get
            {
                if (nextCommand == null)
                {
                    nextCommand = new RelayCommand(param => NextCommandExecute());
                }

                return nextCommand;
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


        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
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

        public ObservableCollection<Tour> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

    }
}
