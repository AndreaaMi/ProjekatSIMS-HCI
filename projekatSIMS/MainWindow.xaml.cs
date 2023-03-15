using projekatSIMS.Model;
using projekatSIMS.Service;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TourService tourService = new TourService();
            foreach(Tour t in tourService.GetAll())
            {
                Debug.WriteLine(t.Id + " " + t.Name + " " + t.Location.Country + " " + t.Location.City + " " + t.Language.ToString() + " " + t.StartingDate.ToString("dd/MM/yyyy") + " " + t.StartingTime + " " + t.Duration + " " + t.MaxNumberOfGuests );
            }
        }
    }
}
