using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace projekatSIMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }
  
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AccommodationSearchView win = new AccommodationSearchView();
            win.Show();
        }

        private void Gost2_Click(object sender, RoutedEventArgs e)
        {
            Gost2 win = new Gost2();
            win.Show();
        }

        private void Vlasnik_Click(object sender, RoutedEventArgs e)
        {
            Vlasnik win = new Vlasnik();
            win.Show();
        }

        private void Vodic_Click(object sender, RoutedEventArgs e)
        {
            Vodic win = new Vodic();
            win.Show();
        }
    }
}
