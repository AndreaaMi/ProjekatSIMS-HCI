using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
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


        private ObservableCollection<Accommodation> accommodationItems = new ObservableCollection<Accommodation>();

        private AccommodationService accommodationService;
        public AllAccommodationsViewModel()
        {
            CurrentImageIndex = 0;
            MoveToNextImageCommand = new RelayCommand(MoveToNextImage);
            MoveToPreviousImageCommand = new RelayCommand(MoveToPreviousImage);
            SetService();
            InitialListViewLoad();
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
