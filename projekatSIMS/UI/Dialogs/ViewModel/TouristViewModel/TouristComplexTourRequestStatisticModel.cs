using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.Model;
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
    internal class TouristComplexTourRequestStatisticModel : ViewModelBase
    {
        private ObservableCollection<ComplexTourRequest> items = new ObservableCollection<ComplexTourRequest>();
        private ComplexTourRequestService complexTourRequestService;
        private ComplexTourRequest selectedRequest;

        private RelayCommand detailsCommand;

        public TouristComplexTourRequestStatisticModel()
        {
            SetService();
            LoadData();
        }
        public void SetService()
        {
            complexTourRequestService = new ComplexTourRequestService();
        }
        private bool CanThisCommandExecute()
        {
            string currentUri = TouristMainWindow.navigationService?.CurrentSource?.ToString();
            return currentUri?.EndsWith("TouristComplexTourRequestsStatisticView.xaml", StringComparison.OrdinalIgnoreCase) == true;
        }

        private void DetailsCommandExecute()
        {
            if (selectedRequest != null)
            {
                TouristMainWindow.navigationService.Navigate(
                    new TouristComplexTourDetailsView(selectedRequest));
                SelectedRequest = null;
            }
            else
            {
                MessageBox.Show("Please select a complex request before proceeding.");
            }
        }
        public void LoadData()
        {
            foreach (ComplexTourRequest item in complexTourRequestService.GetAll())
            {
                    Items.Add(item);
            }
        }
        public ObservableCollection<ComplexTourRequest> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public ComplexTourRequest SelectedRequest
        {
            get { return selectedRequest; }
            set
            {
                selectedRequest = value;
                OnPropertyChanged(nameof(SelectedRequest));
            }
        }

        public RelayCommand DetailsCommand
        {
            get
            {
                if (detailsCommand == null)
                {
                    detailsCommand = new RelayCommand(param => DetailsCommandExecute(), param => CanThisCommandExecute());
                }

                return detailsCommand;
            }
        }
    }
}
