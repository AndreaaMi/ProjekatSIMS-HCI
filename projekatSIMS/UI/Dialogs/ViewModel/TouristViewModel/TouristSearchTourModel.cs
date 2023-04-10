using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.Model;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristSearchTourModel : ViewModelBase
    {
        private RelayCommand backCommand;
        private RelayCommand searchCommand;
        private RelayCommand proceedCommand;

        private ObservableCollection<Tour> items = new ObservableCollection<Tour>();
        private Tour selectedTour;

        private List<ComboBoxData<string>> states = new List<ComboBoxData<string>>();
        private List<ComboBoxData<string>> cities = new List<ComboBoxData<string>>();
        private List<ComboBoxData<string>> durations = new List<ComboBoxData<string>>();
        private List<ComboBoxData<string>> languages = new List<ComboBoxData<string>>();

        //These are the selected strings from the comboboxes and textbox
        private string state;
        private string city;
        private string language;
        private string duration;
        private string guestNumber;


        private TourService tourService;
        public TouristSearchTourModel()
        {
            SetService();
            LoadComboData();
            foreach (Tour entity in tourService.GetAll())
            {
                    Items.Add(entity);
            }
        }
        #region COMMANDS

        private void BackCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristHomeView.xaml", UriKind.Relative));
        }

        private void SearchCommandExecute()
        {
            Items.Clear();
            int guests = int.Parse(GuestNumber);
            foreach (Tour entity in tourService.GetAll())
            {
                if (entity.MaxNumberOfGuests <= guests)
                {
                    Items.Add(entity);
                }
            }
        }

        private void ProceedCommandExecute()
        {
            if (selectedTour != null)
            {
                TouristMainWindow.navigationService.Navigate(
                    new TouristReservationView(selectedTour));
            }
            else
            {
                MessageBox.Show("Please select a tour before proceeding to reservation.");
            }
        }
        public void SetService()
        {
            tourService = new TourService();
        }

        public void LoadComboData()
        {
            LoadComboStates();
            LoadComboCities();
            LoadComboDurations();
            LoadComboLanguages();
        }
        #endregion

        #region LOADING THE DATA

        public void LoadComboStates()
        {
            var tours = tourService.GetAll();
            foreach (Tour tour in tours)
            {
                states.Add(new ComboBoxData<string> { Name = tour.Location.Country.ToString(), Value = tour.Location.Country.ToString() });
            }
            states = states.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        }

        public void LoadComboCities()
        {
            var tours = tourService.GetAll();
            foreach (Tour tour in tours)
            {
                cities.Add(new ComboBoxData<string> { Name = tour.Location.City.ToString(), Value = tour.Location.City.ToString() });
            }
            cities = cities.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        }

        public void LoadComboDurations()
        {
            var tours = tourService.GetAll();
            foreach (Tour tour in tours)
            {
                durations.Add(new ComboBoxData<string> { Name = tour.Duration.ToString(), Value = tour.Duration.ToString() });
            }
            durations = durations.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        }

        public void LoadComboLanguages()
        {
            var tours = tourService.GetAll();
            foreach (Tour tour in tours)
            {
                languages.Add(new ComboBoxData<string> { Name = tour.Language.ToString(), Value = tour.Language.ToString() });
            }
            languages = languages.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        }

        #endregion

        #region SELECTION
        private void StateCombo_SelectionChanged()
        {
           Items.Clear();
           foreach(Tour entity in tourService.GetAll())
            {
                if (entity.Location.Country.ToString().Equals(State))
                {
                    Items.Add(entity);
                }
            }
        }

        private void CityCombo_SelectionChanged()
        {
            Items.Clear();
            foreach (Tour entity in tourService.GetAll())
            {
                if (entity.Location.City.ToString().Equals(City))
                {
                    Items.Add(entity);
                }
            }
        }

        private void DurationCombo_SelectionChanged()
        {
            Items.Clear();
            foreach (Tour entity in tourService.GetAll())
            {
                if (entity.Duration.ToString().Equals(Duration))
                {
                    Items.Add(entity);
                }
            }
        }

        private void LanguageCombo_SelectionChanged()
        {
            Items.Clear();
            foreach (Tour entity in tourService.GetAll())
            {
                if (entity.Language.ToString().Equals(Language))
                {
                    Items.Add(entity);
                }
            }
        }
        #endregion

        #region PROPERTIES

        public ObservableCollection<Tour> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
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

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
                CityCombo_SelectionChanged();
            }
        }
        public string State
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged(nameof(State));
                StateCombo_SelectionChanged();            // TODO - NAPRAVITI SEARCH KOJI SKUPLJA I NJEGA POZIVATI U SVAKOM OD OVIH 
            }
        }
        public string Language
        {
            get { return language; }
            set
            {
                language = value;
                OnPropertyChanged(nameof(Language));
                LanguageCombo_SelectionChanged();
            }
        }

        public string Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged(nameof(Duration));
                DurationCombo_SelectionChanged();
            }
        }

        public string GuestNumber
        {
            get { return guestNumber; }
            set
            {
                guestNumber = value;
                OnPropertyChanged(nameof(GuestNumber));
            }
        }
        public List<ComboBoxData<string>> States
        {
            get { return states; }
            set
            {
                states = value;
                OnPropertyChanged(nameof(States));
            }
        }

        public List<ComboBoxData<string>> Cities
        {
            get { return cities; }
            set
            {
                cities = value;
                OnPropertyChanged(nameof(Cities));
            }
        }

        public List<ComboBoxData<string>> Durations
        {
            get { return durations; }
            set
            {
                durations = value;
                OnPropertyChanged(nameof(Durations));
            }
        }

        public List<ComboBoxData<string>> Languages
        {
            get { return languages; }
            set
            { 
                languages = value;
                OnPropertyChanged(nameof(Languages));
            }
        }

        public RelayCommand BackCommand
        {
            get
            {
                return backCommand ?? (backCommand = new RelayCommand(param => BackCommandExecute()));
            }
        }

        public RelayCommand SearchCommand
        {
            get
            {
                return searchCommand ?? (searchCommand = new RelayCommand(param => SearchCommandExecute()));
            }
        }

        public RelayCommand ProceedCommand
        {
            get
            {
                return proceedCommand ?? (proceedCommand = new RelayCommand(param => ProceedCommandExecute()));
            }
        }
        #endregion

    }
}
