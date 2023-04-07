using projekatSIMS.CompositeComon;
using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristMainWindowModel : ViewModelBase
    {
        private NavigationService navigationService;
        private UserService UserService = new UserService();

        public TouristMainWindowModel(NavigationService navService)
        {
            navigationService = navService;
        }

    }
}
