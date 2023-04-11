using projekatSIMS.CompositeComon;
using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace projekatSIMS.UI.Dialogs.ViewModel.TourGuideViewModel
{
    internal class TourGuideMainWindowModel : ViewModelBase
    {
        private NavigationService navigationService;
        private UserService UserService = new UserService();

        public TourGuideMainWindowModel(NavigationService navService)
        {
            navigationService = navService;
        }

    }
}
