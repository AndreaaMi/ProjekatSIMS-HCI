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

namespace projekatSIMS.UI.Dialogs.View.TouristView
{
    /// <summary>
    /// Interaction logic for TouristSearchTourView.xaml
    /// </summary>
    public partial class TouristSearchTourView : Page
    {
        public TouristSearchTourView()
        {
            InitializeComponent();
            DataContext = new TouristSearchTourModel();
        }

        private void StateComboBox_DropDownOpened(object sender, EventArgs e)
        {
            StateComboBox.Focus();
        }

        private void CityComboBox_DropDownOpened(object sender, EventArgs e)
        {
            CityComboBox.Focus();
        }

        private void LanguageComboBox_DropDownOpened(object sender, EventArgs e)
        {
            LanguageComboBox.Focus();
        }

        private void DurationComboBox_DropDownOpened(object sender, EventArgs e)
        {
            DurationComboBox.Focus();
        }

        private void SlotComboBox_DropDownOpened(object sender, EventArgs e)
        {
            SlotComboBox.Focus();
        }

    }
}
