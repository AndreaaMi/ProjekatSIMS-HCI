using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristHomeModel : ViewModelBase
    {
        private RelayCommand reserveCommand;
        private RelayCommand rateCommand;
        public RelayCommand vouchersCommand;
        public RelayCommand helpCommand;

        public TouristHomeModel()
        {}

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

        private void RateCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristRatingToursView.xaml", UriKind.Relative));
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

        public RelayCommand RateCommand
        {
            get
            {
                if(rateCommand == null)
                {
                    rateCommand = new RelayCommand(param => RateCommandExecute(),param => CanThisCommandExecute());
                }

                return rateCommand;
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


    }
}
