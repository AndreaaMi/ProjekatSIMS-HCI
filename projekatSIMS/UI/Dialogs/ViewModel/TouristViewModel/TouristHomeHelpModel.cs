using projekatSIMS.CompositeComon;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristHomeHelpModel : ViewModelBase
    {
        private RelayCommand gotItCommand;
        private RelayCommand tourRequestCommand;
        private RelayCommand complexTourRequestCommand;
        public TouristHomeHelpModel() { }
        private bool CanThisCommandExecute()
        {
            string currentUri = TouristMainWindow.navigationService?.CurrentSource?.ToString();
            return currentUri?.EndsWith("TouristHomeViewHelp.xaml", StringComparison.OrdinalIgnoreCase) == true;
        }

        private void GotItCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristHomeView.xaml", UriKind.Relative));
        }

        private void TourRequestCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristTourRequestView.xaml", UriKind.Relative));
        }

        private void ComplexTourRequestCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristComplexTourRequestView.xaml", UriKind.Relative));
        }
        public RelayCommand GotItCommand
        {
            get
            {
                if(gotItCommand == null)
                {
                    gotItCommand = new RelayCommand(param=> GotItCommandExecute(), param => CanThisCommandExecute());
                }
                
                return gotItCommand;
            }
        }

        public RelayCommand TourRequestCommand
        {
            get
            {
                if (tourRequestCommand == null)
                {
                    tourRequestCommand = new RelayCommand(param => TourRequestCommandExecute(), param => CanThisCommandExecute());
                }

                return tourRequestCommand;
            }
        }

        public RelayCommand ComplexTourRequestCommand
        {
            get
            {
                if (complexTourRequestCommand == null)
                {
                    complexTourRequestCommand = new RelayCommand(param => ComplexTourRequestCommandExecute(), param => CanThisCommandExecute());
                }

                return complexTourRequestCommand;
            }
        }

    }
}
