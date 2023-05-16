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
        private RelayCommand proceedCommand;
        private RelayCommand openStateComboboxCommand;
        private RelayCommand openCityComboboxCommand;
        private RelayCommand openLanguageComboboxCommand;
        private RelayCommand openDurationComboboxCommand;
        private RelayCommand openSlotComboboxCommand;
        private RelayCommand toursMoveDownCommand;
        private RelayCommand toursMoveUpCommand;
        public RelayCommand helpCommand;

        private bool isStateComboboxOpened;
        private bool isCityComboboxOpened;
        private bool isLanguageComboboxOpened;
        private bool isDurationComboboxOpened;
        private bool isSlotComboboxOpened;


        private ObservableCollection<Tour> items = new ObservableCollection<Tour>();
        private Tour selectedTour;

        private List<ComboBoxData<string>> states = new List<ComboBoxData<string>>();
        private List<ComboBoxData<string>> cities = new List<ComboBoxData<string>>();
        private List<ComboBoxData<string>> durations = new List<ComboBoxData<string>>();
        private List<ComboBoxData<string>> languages = new List<ComboBoxData<string>>();
        private List<ComboBoxData<string>> slots = new List<ComboBoxData<string>>();


        //These are the selected strings from the comboboxes and textbox
        private string state;
        private string city;
        private string language;
        private string duration;
        private string slot;

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
        private bool CanThisCommandExecute()
        {
            string currentUri = TouristMainWindow.navigationService?.CurrentSource?.ToString();
            return currentUri?.EndsWith("TouristSearchTourView.xaml", StringComparison.OrdinalIgnoreCase) == true;
        }
        private void ProceedCommandExecute()
        {
            if (selectedTour != null)
            {
                TouristMainWindow.navigationService.Navigate(
                    new TouristReservationView(selectedTour));
                SelectedTour = null;
            }
            else
            {
                MessageBox.Show("Please select a tour before proceeding to reservation.");
            }
        }
        private void HelpCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristSearchTourHelpView.xaml", UriKind.Relative));
        }
        private void OpenStateComboboxCommandExecute()
        {
            IsStateComboboxOpened = true;
        }
        private void OpenCityComboboxCommandExecute()
        {
            IsCityComboboxOpened = true;
        }
        private void OpenLanguageComboboxCommandExecute()
        {
            IsLanguageComboboxOpened = true;
        }
        private void OpenDurationComboboxCommandExecute()
        {
            IsDurationComboboxOpened = true;
        }
        private void OpenSlotComboboxCommandExecute()
        {
            IsSlotComboboxOpened = true;
        }
        private void ToursMoveDownCommandExecute()
        {
            int selectedIndex = Items.IndexOf(SelectedTour);
            if (selectedIndex < Items.Count - 1)
            {
                SelectedTour = Items[selectedIndex + 1];
            }
        }
        private void ToursMoveUpCommandExecute()
        {
            int selectedIndex = Items.IndexOf(SelectedTour);
            if (selectedIndex > 0)
            {
                SelectedTour = Items[selectedIndex - 1];
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
            LoadComboSlots();
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

        public void LoadComboSlots()
        {
            slots.Add(new ComboBoxData<string> { Name = "10", Value = "10"});
            slots.Add(new ComboBoxData<string> { Name = "20", Value = "20" });
            slots.Add(new ComboBoxData<string> { Name = "30", Value = "30" });
            slots.Add(new ComboBoxData<string> { Name = "40", Value = "40" });
            slots.Add(new ComboBoxData<string> { Name = "50", Value = "50" });
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
        private void SlotCombo_SelectionChanged()
        {
            Items.Clear();
            foreach (Tour entity in tourService.GetAll())
            {
                if (entity.MaxNumberOfGuests <= int.Parse(Slot))
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
        public string Slot
        {
            get { return slot; }
            set
            {
                slot = value;
                OnPropertyChanged(nameof(Slot));
                SlotCombo_SelectionChanged();
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
        public List<ComboBoxData<string>> Slots
        {
            get { return slots; }
            set
            {
                slots = value;
                OnPropertyChanged(nameof(Slots));
            }
        }

        public bool IsStateComboboxOpened
        {
            get { return isStateComboboxOpened; }
            set
            {
                isStateComboboxOpened = value;
                OnPropertyChanged(nameof(IsStateComboboxOpened));
            }
        }
        public bool IsCityComboboxOpened
        {
            get { return isCityComboboxOpened; }
            set
            {
                isCityComboboxOpened = value;
                OnPropertyChanged(nameof(IsCityComboboxOpened));
            }
        }
        public bool IsLanguageComboboxOpened
        {
            get { return isLanguageComboboxOpened; }
            set
            {
                isLanguageComboboxOpened = value;
                OnPropertyChanged(nameof(IsLanguageComboboxOpened));
            }
        }
        public bool IsDurationComboboxOpened
        {
            get { return isDurationComboboxOpened; }
            set
            {
                isDurationComboboxOpened = value;
                OnPropertyChanged(nameof(IsDurationComboboxOpened));
            }
        }
        public bool IsSlotComboboxOpened
        {
            get { return isSlotComboboxOpened; }
            set
            {
                isSlotComboboxOpened = value;
                OnPropertyChanged(nameof(IsSlotComboboxOpened));
            }
        }   
        public RelayCommand ProceedCommand
        {
            get
            {
                return proceedCommand ?? (proceedCommand = new RelayCommand(param => ProceedCommandExecute(), param => CanThisCommandExecute()));
            }
        }

        public RelayCommand OpenStateComboboxCommand
        {
            get
            {
                return openStateComboboxCommand ?? (openStateComboboxCommand = new RelayCommand(param => OpenStateComboboxCommandExecute(), param => CanThisCommandExecute()));
            }
        }
        public RelayCommand OpenCityComboboxCommand
        {
            get
            {
                return openCityComboboxCommand ?? (openCityComboboxCommand = new RelayCommand(param => OpenCityComboboxCommandExecute(), param => CanThisCommandExecute()));
            }
        }
        public RelayCommand OpenLanguageComboboxCommand
        {
            get
            {
                return openLanguageComboboxCommand ?? (openLanguageComboboxCommand = new RelayCommand(param => OpenLanguageComboboxCommandExecute(), param => CanThisCommandExecute()));
            }
        }
        public RelayCommand OpenDurationComboboxCommand
        {
            get
            {
                return openDurationComboboxCommand ?? (openDurationComboboxCommand = new RelayCommand(param => OpenDurationComboboxCommandExecute(), param => CanThisCommandExecute()));
            }
        }
        public RelayCommand OpenSlotComboboxCommand
        {
            get
            {
                return openSlotComboboxCommand ?? (openSlotComboboxCommand = new RelayCommand(param => OpenSlotComboboxCommandExecute(), param => CanThisCommandExecute()));
            }
        }
        public RelayCommand ToursMoveDownCommand
        {
            get
            {
                return toursMoveDownCommand ?? (toursMoveDownCommand = new RelayCommand(param => ToursMoveDownCommandExecute(), param => CanThisCommandExecute()));
            }
        }

        public RelayCommand ToursMoveUpCommand
        {
            get
            {
                return toursMoveUpCommand ?? (toursMoveUpCommand = new RelayCommand(param => ToursMoveUpCommandExecute(), param => CanThisCommandExecute()));
            }
        }
        public RelayCommand HelpCommand
        {
            get
            {
                if (helpCommand == null)
                {
                    helpCommand = new RelayCommand(param => HelpCommandExecute(), param => CanThisCommandExecute());
                }

                return helpCommand;
            }
        }

        #endregion

    }
}
