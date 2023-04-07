using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.Model;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
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

        private Tour selectedItem;

        private List<ComboBoxData<string>> states = new List<ComboBoxData<string>>();
        private List<ComboBoxData<string>> cities = new List<ComboBoxData<string>>();
        private List<ComboBoxData<string>> durations = new List<ComboBoxData<string>>();
        private List<ComboBoxData<string>> languages = new List<ComboBoxData<string>>();
    

        private TourService tourService;
        public TouristSearchTourModel()
        {
            SetService();
            LoadComboData();
            
        }

        private void BackCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristHomeView.xaml", UriKind.Relative));
        }

        public RelayCommand BackCommand
        {
            get
            {
                return backCommand ?? (backCommand = new RelayCommand(param => BackCommandExecute()));
            }
        }

        public Tour SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
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
        
        public void LoadComboStates()
        {
            var tours = tourService.GetAll();
            foreach( Tour tour in tours )
            {
               states.Add(new ComboBoxData<string> { Name = tour.Location.Country.ToString() });
            }
            states = states.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        }

        public void LoadComboCities()
        {
            var tours = tourService.GetAll();
            foreach (Tour tour in tours)
            {
                cities.Add(new ComboBoxData<string> { Name = tour.Location.City.ToString() });
            }
            cities= cities.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        }

        public void LoadComboDurations()
        {
            var tours = tourService.GetAll();
            foreach (Tour tour in tours)
            {
                durations.Add(new ComboBoxData<string> { Name = tour.Duration.ToString() });
            }
            durations = durations.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        }

        public void LoadComboLanguages()
        {
            var tours = tourService.GetAll();
            foreach (Tour tour in tours)
            {
                languages.Add(new ComboBoxData<string> { Name = tour.Language.ToString() });
            }
            languages = languages.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        }
    }
}
