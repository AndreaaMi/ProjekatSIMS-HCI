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

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristComplexTourDetailsModel : ViewModelBase
    {
        private RelayCommand gotItCommand;

        private ComplexTourRequest complexRequest;
        private ObservableCollection<TourRequest> items = new ObservableCollection<TourRequest>();
        private TourRequestService tourRequestService;

        public TouristComplexTourDetailsModel(ComplexTourRequest request)
        {
            complexRequest = request;
            SetService();
            FillItems();
        }
        private bool CanThisCommandExecute()
        {
            string currentUri = TouristMainWindow.navigationService?.CurrentSource?.ToString() + "ubicuse";
            if (currentUri.Equals("ubicuse"))
            {
                return true;
            }
            return false;
        }
        private void GotItCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristComplexTourRequestsStatisticView.xaml", UriKind.Relative));
        }
        private void SetService()
        {
            tourRequestService = new TourRequestService();
        }
        private void FillItems()
        {
            string ids = complexRequest.ExportRequests(complexRequest.Requests);
            if (ids == "NULL")
                return;

            var idList = ids.Split('_');

            foreach (TourRequest request in tourRequestService.GetAll())
            {
                if (idList.Contains(request.Id.ToString()))
                {
                    Items.Add(request);
                }
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
        public RelayCommand GotItCommand
        {
            get
            {
                if (gotItCommand == null)
                {
                    gotItCommand = new RelayCommand(param => GotItCommandExecute(), param => CanThisCommandExecute());
                }

                return gotItCommand;
            }
        }

    }
}
