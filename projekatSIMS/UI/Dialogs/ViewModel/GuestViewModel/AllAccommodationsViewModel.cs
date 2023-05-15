using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace projekatSIMS.UI.Dialogs.ViewModel.GuestViewModel
{
    public class AllAccommodationsViewModel : ViewModelBase
    {

        private int currentImageIndex;
        public int CurrentImageIndex
        {
            get { return currentImageIndex; }
            set
            {
                currentImageIndex = value;
                OnPropertyChanged(nameof(CurrentImageIndex));
                OnPropertyChanged(nameof(CurrentImageUrl));
            }
        }

        public string CurrentImageUrl
        {
            get { return SelectedAccommodation?.ImageUrls.ElementAtOrDefault(CurrentImageIndex); }
        }

        public ICommand MoveToNextImageCommand { get; set; }
        public ICommand MoveToPreviousImageCommand { get; set; }
        public ICommand BackCommand { get; set; }

        private void MoveToNextImage(object paremater)
        {
            if (SelectedAccommodation != null && CurrentImageIndex < SelectedAccommodation.ImageUrls.Count - 1)
            {
                CurrentImageIndex++;
                OnPropertyChanged(nameof(CurrentImageUrl));
            }
        }

        private void MoveToPreviousImage(object paremater)
        {
            if (SelectedAccommodation != null && CurrentImageIndex > 0)
            {
                CurrentImageIndex--;
                OnPropertyChanged(nameof(CurrentImageUrl));
            }
        }


        private UserControl _selectedView;

        public UserControl SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                OnPropertyChanged(nameof(SelectedView));
            }
        }
        private void OnSelectedAccommodationChanged()
        {
            CurrentImageIndex = 0;
            OnPropertyChanged(nameof(CurrentImageUrl));
        }

        public ICommand ShowAllAccommodationsHelpCommand { get; private set; }


        private ObservableCollection<Accommodation> accommodationItems = new ObservableCollection<Accommodation>();

        private AccommodationService accommodationService;
        public AllAccommodationsViewModel()
        {
            BackCommand = new RelayCommand(BackControl);
            CurrentImageIndex = 0;
            ShowAllAccommodationsHelpCommand = new RelayCommand(ShowAllAccommodationsHelpControl);
            MoveToNextImageCommand = new RelayCommand(MoveToNextImage);
            MoveToPreviousImageCommand = new RelayCommand(MoveToPreviousImage);
            SetService();
            InitialListViewLoad();
        }

        private void BackControl(object parameter)
        {
            SelectedView = new GuestPageView();
        }
        private void ShowAllAccommodationsHelpControl(object parameter)
        {
            SelectedView = new AllAccommodationHelpView();
        }
        public void SetService()
        {
            accommodationService = new AccommodationService();
        }

        private void InitialListViewLoad()
        {
            foreach (Accommodation accommodation in accommodationService.GetAll().Cast<Accommodation>())
            {
                AccommodationItems.Add(accommodation);
            }
        }
        public ObservableCollection<Accommodation> AccommodationItems
        {
            get { return accommodationItems; }
            set
            {
                accommodationItems = value;
                OnPropertyChanged(nameof(accommodationItems));
            }
        }

        private Accommodation selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get { return selectedAccommodation; }
            set
            {
                if (selectedAccommodation != value)
                {
                    selectedAccommodation = value;
                    OnPropertyChanged(nameof(SelectedAccommodation));

                    OnSelectedAccommodationChanged(); // Poziv metode za reakciju na promenu izabranog smještaja
                }
            }
        }

    }
}
