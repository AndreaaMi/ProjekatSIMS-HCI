using projekatSIMS.UI.Dialogs.ViewModel.TourGuideViewModel;
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
    /// Interaction logic for TourGuideComplexTourPageView.xaml
    /// </summary>
    public partial class TourGuideComplexTourPageView : Page
    {
        public TourGuideComplexTourPageView()
        {
            InitializeComponent();
            DataContext = new TourGuideComplexTourPageModel();
        }
    }
}
