using projekatSIMS.CompositeComon;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristHomeModel : ViewModelBase
    {
        private RelayCommand reserveCommand;

        public TouristHomeModel()
        {
             
        }

        private void ReserveCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristSearchTourView.xaml",UriKind.Relative));
        }

        public RelayCommand ReserveCommand
        {
            get
            {
                if(reserveCommand == null)
                {
                    reserveCommand = new RelayCommand(param => ReserveCommandExecute());
                }

                return reserveCommand;
            }
        }


    }
}
