using LiveCharts;
using LiveCharts.Wpf;
using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristTourRequestStatisticModel : ViewModelBase
    {
        private ObservableCollection<TourRequest> items = new ObservableCollection<TourRequest>();
        private List<ComboBoxData<string>> years = new List<ComboBoxData<string>>();
        private string year;
        private string acceptedTours;
        private string averageNumberOfPeople;
        private string declinedTours;
        
        private SeriesCollection languageSeriesCollection;
        private SeriesCollection stateSeriesCollection;
        private SeriesCollection citySeriesCollection;
        
        private Func<string, string> languageValues;
        private Func<string, string> stateValues;
        private Func<string, string> cityValues;


        private TourRequestService tourRequestService;
        private UserService userService;
        public TouristTourRequestStatisticModel()
        {
            SetService();
            LoadData();
            Year = "All time";
        }

        #region FUNCTIONS
        public void SetService()
        {
            tourRequestService = new TourRequestService();
            userService = new UserService();
        }

        public void LoadData()
        {
            foreach(TourRequest item in tourRequestService.GetAll())
            {
                if(item.GuestId == userService.GetLoginUser().Id)
                {
                    Items.Add(item);
                }
            }
            years.Add(new ComboBoxData<string> { Name = "2023", Value = "2023" });
            years.Add(new ComboBoxData<string> { Name = "2024", Value = "2024" });
            years.Add(new ComboBoxData<string> { Name = "All time", Value = "All time" });
        }

        public void LoadStatistics(string selectedYear)
        {
            if(selectedYear.Equals("All time"))
            {
                selectedYear = "";
            }
            AcceptedTours = "Percentage of tours i suggested that got accepted: " + GetAcceptedToursPercentage(selectedYear);
            DeclinedTours = "Percentage of tours i suggested that got declined: " + GetDeclinedToursPercentage(selectedYear);
            AverageNumberOfPeople = "Average number of people in accepted requests: " + GetAverageGuestNumberOnAcceptedTours(selectedYear);
            DrawLanguageGraph(selectedYear);
            DrawStateGraph(selectedYear);
            DrawCityGraph(selectedYear);
        }
        #endregion

        #region DRAWING THE GRAPHS
        public void DrawLanguageGraph(string selectedYear)
        {
            LanguageSeriesCollection = new SeriesCollection();
            foreach (Language language in Enum.GetValues(typeof(Language)))
            {
                LanguageSeriesCollection.Add(new ColumnSeries
                {
                    Title = language.ToString(),
                    Values = new ChartValues<double> { GetAllTourRequestsByLanguage(language, selectedYear) }
                });
            }
        }

        public void DrawStateGraph(string selectedYear)
        {
            StateSeriesCollection = new SeriesCollection();
            foreach (string state in tourRequestService.GetDifferentStates())
            {
                StateSeriesCollection.Add(new ColumnSeries
                {
                    Title = state,
                    Values = new ChartValues<double> { GetAllTourRequestsByState(state, selectedYear) }
                });
            }
        }
        public void DrawCityGraph(string selectedYear)
        {
            CitySeriesCollection = new SeriesCollection();
            foreach (string city in tourRequestService.GetDifferentCities())
            {
                CitySeriesCollection.Add(new ColumnSeries
                {
                    Title = city,
                    Values = new ChartValues<double> { GetAllTourRequestsByCity(city, selectedYear) }
                });
            }
        }
        
        private double GetAllTourRequestsByLanguage(Language language,string selectedYear)
        {
            double number = 0;
            foreach(TourRequest request in tourRequestService.GetAll())
            {
                if (request.Language.Equals(language.ToString()) && request.StartDate.ToString().Contains(selectedYear))
                {
                    number++;
                }
            }
            return number;
        }
        private double GetAllTourRequestsByState(string state, string selectedYear)
        {
            double number = 0;
            foreach (TourRequest request in tourRequestService.GetAll())
            {
                if (request.Location.Country.Equals(state) && request.StartDate.ToString().Contains(selectedYear))
                {
                    number++;
                }
            }
            return number;
        }
        private double GetAllTourRequestsByCity(string city, string selectedYear)
        {
            double number = 0;
            foreach (TourRequest request in tourRequestService.GetAll())
            {
                if (request.Location.City.Equals(city) && request.StartDate.ToString().Contains(selectedYear))
                {
                    number++;
                }
            }
            return number;
        }
        #endregion

        #region CALCULATING THE PERCENTAGES
        public string GetAcceptedToursPercentage(string selectedYear)
        {
            string acceptedToursPercentage = "";
            int acceptedToursCounter = 0;
            int toursCounter = 0;
            string yearToCompare = selectedYear;
            if(selectedYear.Equals("All time"))
            {
                yearToCompare = "";
            }
            foreach(TourRequest request in tourRequestService.GetAll().ToList())
            {
                if(request.GuestId == userService.GetLoginUser().Id && request.StartDate.Year.ToString().Contains(yearToCompare))
                {
                    if(request.Status == TourRequestStatus.ACCEPTED)
                    {
                        acceptedToursCounter++;
                    }
                    toursCounter++;
                }
            }
            if(toursCounter > 0)
            {
                double percentage = Math.Round(((double)acceptedToursCounter / toursCounter) * 100,2);
                acceptedToursPercentage += percentage.ToString() + "%";
            }
            return acceptedToursPercentage;
        }
        public string GetDeclinedToursPercentage(string selectedYear)
        {
            string declinedToursPercentage = "";
            int declinedToursCounter = 0;
            int toursCounter = 0;
            string yearToCompare = selectedYear;
            if (selectedYear.Equals("All time"))
            {
                yearToCompare = "";
            }
            foreach (TourRequest request in tourRequestService.GetAll().ToList())
            {
                if (request.GuestId == userService.GetLoginUser().Id && request.StartDate.Year.ToString().Contains(yearToCompare))
                {
                    if (request.Status == TourRequestStatus.REJECTED)
                    {
                        declinedToursCounter++;
                    }
                    toursCounter++;
                }
            }
            if (toursCounter > 0)
            {
                double percentage = Math.Round(((double)declinedToursCounter / toursCounter) * 100, 2);
                declinedToursPercentage += percentage.ToString() + "%";
            }
            return declinedToursPercentage;
        }

        public string GetAverageGuestNumberOnAcceptedTours(string selectedYear)
        {
            double averageGuestNumber = 0;
            string yearToCompare = selectedYear;
            int toursCounter = 0;
            if (selectedYear.Equals("All time"))
            {
                yearToCompare = "";
            }
            foreach(TourRequest request in tourRequestService.GetAll().ToList())
            {
                if(request.GuestId == userService.GetLoginUser().Id && request.StartDate.Year.ToString().Contains(yearToCompare) && request.Status == TourRequestStatus.ACCEPTED)
                {
                        averageGuestNumber += request.GuestNumber;   
                        toursCounter++;
                }
            }
            if(toursCounter > 0)
            {
                averageGuestNumber = Math.Round((averageGuestNumber / toursCounter), 2);
            }

            return averageGuestNumber.ToString();
        }

        #endregion

        #region PROPERTIES
        public ObservableCollection<TourRequest> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public List<ComboBoxData<string>> Years
        {
            get { return years; }
            set
            {
                years = value;
                OnPropertyChanged(nameof(Years));
            }
        }

        public string Year
        {
            get { return year; }
            set
            {
                year = value;
                OnPropertyChanged(nameof(Year));
                LoadStatistics(Year);
            }
        }

        public SeriesCollection LanguageSeriesCollection
        {
            get { return languageSeriesCollection; }
            set
            {
                languageSeriesCollection = value;
                OnPropertyChanged(nameof(LanguageSeriesCollection));
            }
        }
        public SeriesCollection StateSeriesCollection
        {
            get { return stateSeriesCollection; }
            set
            {
                stateSeriesCollection = value;
                OnPropertyChanged(nameof(StateSeriesCollection));
            }
        }
        public SeriesCollection CitySeriesCollection
        {
            get { return citySeriesCollection; }
            set
            {
                citySeriesCollection = value;
                OnPropertyChanged(nameof(CitySeriesCollection));
            }
        }
        public Func<string,string> LanguageValues
        {
            get { return languageValues; }
            set
            {
                languageValues = value;
                OnPropertyChanged(nameof(LanguageValues));
            }
        }
        public Func<string, string> StateValues
        {
            get { return stateValues; }
            set
            {
                stateValues = value;
                OnPropertyChanged(nameof(StateValues));
            }
        }
        public Func<string, string> CityValues
        {
            get { return cityValues; }
            set
            {
                cityValues = value;
                OnPropertyChanged(nameof(CityValues));
            }
        }

        public string AcceptedTours
        {
            get { return acceptedTours; }
            set
            {
                acceptedTours = value;
                OnPropertyChanged(nameof(AcceptedTours));
            }
        }

        public string AverageNumberOfPeople
        {
            get { return averageNumberOfPeople; }
            set
            {
                averageNumberOfPeople = value;
                OnPropertyChanged(nameof(AverageNumberOfPeople));
            }
        }

        public string DeclinedTours
        {
            get { return declinedTours; }
            set
            {
                declinedTours = value;
                OnPropertyChanged(nameof(DeclinedTours));
            }
        }
        #endregion
    }
}
