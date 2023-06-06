using projekatSIMS.CompositeComon;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristSearchTourHelpModel : ViewModelBase
    {
        public RelayCommand gotItCommand;
        public TouristSearchTourHelpModel() { }
        private bool CanThisCommandExecute()
        {
            string currentUri = TouristMainWindow.navigationService?.CurrentSource?.ToString();
            return currentUri?.EndsWith("TouristSearchTourHelpView.xaml", StringComparison.OrdinalIgnoreCase) == true;
        }

        private void GotItCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristSearchTourView.xaml", UriKind.Relative));
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
