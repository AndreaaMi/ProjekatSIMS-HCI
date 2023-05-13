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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace projekatSIMS.UI.Dialogs.View.GuestView
{
    /// <summary>
    /// Interaction logic for RateNowView.xaml
    /// </summary>
    public partial class RateNowView : UserControl
    {
        public RateNowView()
        {
            InitializeComponent();
            DataContext = new RateNowViewModel();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
