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
using System.Windows;
using System.Windows.Navigation;

namespace projekatSIMS.UI.Dialogs.ViewModel.TourGuideViewModel
{
    internal class TourGuideToursTodayModel : ViewModelBase
    {
        private RelayCommand nextCommand;

        private TourService tourService;
        private KeyPointsService keyPointsService;

        private Tour selectedItem;
        private ObservableCollection<Tour> items = new ObservableCollection<Tour>();


        public TourGuideToursTodayModel()
        {
            SetService();
            
            foreach (Tour tour in tourService.GetAll())
            {
                if (tour.StartingDate == DateTime.Today)
                {
                    Items.Add(tour);
                }
                
                
            }

            
        }

        public void SetService()
        {
            tourService = new TourService();
            keyPointsService = new KeyPointsService();
        }

        private void NextCommandExecute()
        {
            if (selectedItem != null)
            {
                foreach (KeyPoints kp in keyPointsService.GetAll())
                {
                    if(kp.AssociatedTour == selectedItem.Id)
                    {
                        kp.IsActive = true;
                        keyPointsService.Edit(kp);
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a tour before proceeding to reservation.");
            }
            // TourGuideMainWindow.navigationService.Navigate(
            //  new Uri("UI/Dialogs/View/TourGuideView/TourGuideToursToday.xaml", UriKind.Relative));
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

        public Tour SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
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
