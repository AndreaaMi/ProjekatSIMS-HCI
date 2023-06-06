using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristComplexTourRequestModel : ViewModelBase
    {
        private RelayCommand addCommand;
        private RelayCommand submitCommand;
        private ObservableCollection<TourRequest> items = new ObservableCollection<TourRequest>();

        private string state;
        private string city;
        private string description;
        private string language;
        private int guestNumber;
        private DateTime startDate;
        private DateTime endDate;
        private DateTime defaultStartDate = DateTime.Today;

        private TourRequestService tourRequestService;
        private UserService userService;
        private ComplexTourRequestService complexTourRequestService;

        public TouristComplexTourRequestModel()
        {
            SetService();
        }

        public void SetService()
        {
            tourRequestService = new TourRequestService();
            userService = new UserService();
            complexTourRequestService = new ComplexTourRequestService();
        }
        private bool CanThisCommandExecute()
        {
            string currentUri = TouristMainWindow.navigationService?.CurrentSource?.ToString();
            return currentUri?.EndsWith("TouristComplexTourRequestView.xaml", StringComparison.OrdinalIgnoreCase) == true;
        }

        public void AddCommandExecute()
        {
            if (City == null || State == null || Language == null)
            {
                MessageBox.Show("Please fill all the necessary fields!", " ", MessageBoxButton.OK);
                return;
            }
            if (EndDate < StartDate)
            {
                MessageBox.Show("Please enter a valid date range!", " ", MessageBoxButton.OK);
                return;
            }
            if (GuestNumber <= 0)
            {
                MessageBox.Show("Please enter a valid guest number!", " ", MessageBoxButton.OK);
                return;
            }
            CreateRequest();
        }

        public void SubmitCommandExecute()
        {
            if(Items.Count < 2)
            {
                MessageBox.Show("You have to add at least two tours before proceeding!", " ", MessageBoxButton.OK);
                return;
            }
            ComplexTourRequest complexTourRequest = new ComplexTourRequest();
            complexTourRequest.Id = complexTourRequestService.GenerateId();
            foreach (TourRequest item in Items)
            {
                complexTourRequest.Requests.Add(item);
            }
            complexTourRequest.Status = TourRequestStatus.PENDING;
            complexTourRequestService.Add(complexTourRequest);
            MessageBoxResult result = MessageBox.Show("Your request has been noted!", " ", MessageBoxButton.OK);
            if (result == MessageBoxResult.OK)
            {
                TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristHomeView.xaml", UriKind.Relative));
            }
        }
        public void CreateRequest()
        {
            Location location = new Location();
            location.City = City;
            location.Country = State;
            TourRequest tourRequest = new TourRequest();
            tourRequest.Id = tourRequestService.GenerateId();
            tourRequest.GuestId = userService.GetLoginUser().Id;
            tourRequest.Location = location;
            tourRequest.Description = Description;
            tourRequest.Language = Language;
            tourRequest.StartDate = StartDate;
            tourRequest.EndDate = EndDate;
            tourRequest.GuestNumber = GuestNumber;
            tourRequest.Status = TourRequestStatus.PENDING;
            tourRequest.IsPartOfComplexTour = true;
            tourRequestService.Add(tourRequest);
            Items.Add(tourRequest);
        }
        public string State
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged(nameof(State));
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
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Language
        {
            get { return language; }
            set
            {
                language = value;
                OnPropertyChanged(nameof(Language));
            }
        }
        public int GuestNumber
        {
            get { return guestNumber; }
            set
            {
                guestNumber = value;
                OnPropertyChanged(nameof(GuestNumber));
            }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(param => AddCommandExecute(), param => CanThisCommandExecute()));
            }
        }
        public RelayCommand SubmitCommand
        {
            get
            {
                return submitCommand ?? (submitCommand = new RelayCommand(param => SubmitCommandExecute(), param => CanThisCommandExecute()));
            }
        }
        public DateTime DefaultStartDate
        {
            get { return defaultStartDate; }
            set
            {
                defaultStartDate = value;
                OnPropertyChanged(nameof(DefaultStartDate));
            }
        }
        public ObservableCollection<TourRequest> Items
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
