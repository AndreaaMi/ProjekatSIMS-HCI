using projekatSIMS.UI.Dialogs.ViewModel;
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
using System.Windows.Shapes;

namespace projekatSIMS.UI.Dialogs.View
{
    /// <summary>
    /// Interaction logic for AccommodationSearchView.xaml
    /// </summary>
    public partial class AccommodationSearchView : Window
    {
        public AccommodationSearchView()
        {
            DataContext = new AccommodationSearchViewModel();
            InitializeComponent();
        }
    }
}
