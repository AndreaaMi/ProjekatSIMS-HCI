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
        public RelayCommand gotItCommand;
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
    }
}
