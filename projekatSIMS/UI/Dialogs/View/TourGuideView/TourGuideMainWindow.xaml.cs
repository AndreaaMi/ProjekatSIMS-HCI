using projekatSIMS.UI.Dialogs.View.TouristView;
using projekatSIMS.UI.Dialogs.ViewModel.TourGuideViewModel;
using projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace projekatSIMS.UI.Dialogs.View.TourGuideView
{
    /// <summary>
    /// Interaction logic for TourGuideMainWindow.xaml
    /// </summary>
   
    public partial class TourGuideMainWindow : Window
    {
        private TourGuideMainWindowModel tourGuideMainWindowModel;
        public static NavigationService navigationService;
        private static volatile TourGuideMainWindow instance;
        public TourGuideMainWindow()
        {
            InitializeComponent();
            navigationService = this.frame.NavigationService;
            tourGuideMainWindowModel = new TourGuideMainWindowModel(navigationService);
            DataContext = tourGuideMainWindowModel;
            
        }

        public static TourGuideMainWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    return instance;
                }
                instance = new TourGuideMainWindow();
                return instance;
            }
        }
    }
}
