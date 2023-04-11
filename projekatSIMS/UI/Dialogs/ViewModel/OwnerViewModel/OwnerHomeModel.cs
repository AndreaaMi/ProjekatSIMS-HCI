using projekatSIMS.CompositeComon;
using projekatSIMS.UI.Dialogs.View.OwnerView;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.OwnerHomeModel
{
    internal class OwnerHomeModel : ViewModelBase
    {
        private RelayCommand reserveCommand;

        public OwnerHomeModel()
        {

        }

        private void ReserveCommandExecute()
        {
            OwnerMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/OwnerView/kt2.xaml", UriKind.Relative));
        }

        public RelayCommand ReserveCommand
        {
            get
            {
                if (reserveCommand == null)
                {
                    reserveCommand = new RelayCommand(param => ReserveCommandExecute());
                }

                return reserveCommand;
            }
        }


    }
}
