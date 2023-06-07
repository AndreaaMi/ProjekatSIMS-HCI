using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristHomeModel : ViewModelBase
    {
        private RelayCommand goToProfilePageCommand;
        private RelayCommand reserveCommand;
        private RelayCommand activeToursCommand;
        private RelayCommand vouchersCommand;
        private RelayCommand helpCommand;
        private RelayCommand requestTourCommand;
        private RelayCommand requestsCommand;
        private RelayCommand complexRequestsCommand;

        private UserService userService;
        private TourRequestService requestService;
        private TourService tourService;

        public TouristHomeModel()
        {
            SetService();
            DisplayNotification(GetPendingRequests());
        }

        public void SetService()
        {
            userService = new UserService();
            requestService = new TourRequestService();
            tourService = new TourService();
        }

        public void DisplayNotification(List<TourRequest> requests)
        {
            requests = requests.ToList();
            foreach(TourRequest request in requests)
            {
                foreach(Tour tour in tourService.GetAll())
                {
                    if(request.Location.Country.Equals(tour.Location.Country) && request.Location.City.Equals(tour.Location.City) && request.Language.Equals(tour.Language.ToString()))
                    {
                        request.Status = TourRequestStatus.ACCEPTED;
                        DataContext.Instance.Save();
                        TouristMainWindow.navigationService.Navigate(
                            new TouristHomeViewNotification(tour));
                    }
                }
            }
        }

        public List<TourRequest> GetPendingRequests()
        {
            List<TourRequest> pendingRequests = new List<TourRequest>();
            foreach(TourRequest request in requestService.GetAll())
            {
                if(request.GuestId == userService.GetLoginUser().Id && request.Status == TourRequestStatus.PENDING)
                {
                    pendingRequests.Add(request);
                }
            }
            return pendingRequests;
        }

        private bool CanThisCommandExecute()
        {
            string currentUri = TouristMainWindow.navigationService?.CurrentSource?.ToString();
            return currentUri?.EndsWith("TouristHomeView.xaml", StringComparison.OrdinalIgnoreCase) == true;
        }

        private void ReserveCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristSearchTourView.xaml", UriKind.Relative));
        }
        private void VouchersCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristVouchersView.xaml", UriKind.Relative));
        }

        private void HelpCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristHomeViewHelp.xaml", UriKind.Relative));
        }

        private void ActiveToursCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristActiveToursView.xaml", UriKind.Relative));
        }
        private void GoToProfilePageCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristHomeView.xaml", UriKind.Relative));
        }

        private void RequestTourCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
               new Uri("UI/Dialogs/View/TouristView/TouristTourRequestView.xaml", UriKind.Relative));
        }

        private void RequestsCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
               new Uri("UI/Dialogs/View/TouristView/TouristTourRequestStatisticsView.xaml", UriKind.Relative));
        }

        private void ComplexRequestsCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
               new Uri("UI/Dialogs/View/TouristView/TouristComplexTourRequestsStatisticView.xaml", UriKind.Relative));
        }
        public void Dispose()
        {
        }

        public RelayCommand ReserveCommand
        {
            get
            {
                if(reserveCommand == null)
                {
                    reserveCommand = new RelayCommand(param => ReserveCommandExecute(),param => CanThisCommandExecute());
                }

                return reserveCommand;
            }
        }

        public RelayCommand VouchersCommand
        {
            get
            {
                if (vouchersCommand == null)
                {
                    vouchersCommand = new RelayCommand(param => VouchersCommandExecute(), param => CanThisCommandExecute());
                }

                return vouchersCommand;
            }
        }

        public RelayCommand HelpCommand
        {
            get
            {
                if(helpCommand == null)
                {
                    helpCommand = new RelayCommand(param => HelpCommandExecute(),param => CanThisCommandExecute());
                }

                return helpCommand;
            }
        }

        public RelayCommand ActiveToursCommand
        {
            get
            {
                if (activeToursCommand == null)
                {
                    activeToursCommand = new RelayCommand(param => ActiveToursCommandExecute(), param => CanThisCommandExecute());
                }

                return activeToursCommand;
            }
        }

        public RelayCommand RequestTourCommand
        {
            get
            {
                if (requestTourCommand == null)
                {
                    requestTourCommand = new RelayCommand(param => RequestTourCommandExecute(), param => CanThisCommandExecute());
                }

                return requestTourCommand;
            }
        }

        public RelayCommand GoToProfilePageCommand
        {
            get
            {
                return goToProfilePageCommand ?? (goToProfilePageCommand = new RelayCommand(param => GoToProfilePageCommandExecute()));
            }
        }

        public RelayCommand RequestsCommand
        {
            get
            {
                return requestsCommand ?? (requestsCommand = new RelayCommand(param => RequestsCommandExecute(), param => CanThisCommandExecute()));
            }
        }
        public RelayCommand ComplexRequestsCommand
        {
            get
            {
                return complexRequestsCommand ?? (complexRequestsCommand = new RelayCommand(param => ComplexRequestsCommandExecute(), param => CanThisCommandExecute()));
            }
        }


    }
}
