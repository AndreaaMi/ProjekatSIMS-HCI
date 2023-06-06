using projekatSIMS.UI.Dialogs.ViewModel.GuestViewModel;
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

namespace projekatSIMS.UI.Dialogs.View.GuestView
{
    /// <summary>
    /// Interaction logic for GuestMainView.xaml
    /// </summary>
    public partial class GuestMainView : Window
    {
        public GuestMainView()
        {
            InitializeComponent();
            DataContext = new GuestMainViewModel();

        }
    }
}
