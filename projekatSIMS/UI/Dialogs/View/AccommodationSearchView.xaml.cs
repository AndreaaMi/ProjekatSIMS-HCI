using projekatSIMS.Model;
using projekatSIMS.Service;
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
                InitializeComponent();
                //UBACIVANJE PARAMETARA U COMBOX
                AccommodationService accommodationService = new AccommodationService();
                var accommodations = accommodationService.GetAll();
                TypesComboBox.ItemsSource = accommodations.Where(a => a is Accommodation).Select(a => ((Accommodation)a).Type).Distinct();
                CityComboBox.ItemsSource = accommodations.Where(a => a is Accommodation && ((Accommodation)a).Location != null).Select(a => ((Accommodation)a).Location.City).Distinct();

        }


        private void DisplayAccommodations_Click(object sender, RoutedEventArgs e)
        {

            MyListBox.Items.Clear();

            AccommodationService accommodationService = new AccommodationService();

            foreach (Accommodation entity in accommodationService.GetAll())
            {
                MyListBox.Items.Add(entity.Id + " " + entity.Name + " " + entity.Location.City + " " + entity.Location.Country + " " + entity.GuestLimit + " " + entity.MinimalStay + " " + entity.CancelationLimit);
            }
        }

        private void TypesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyListBox.Items.Clear();
            string selectedType = TypesComboBox.SelectedItem.ToString();
            AccommodationService accommodationService = new AccommodationService();
            foreach (Accommodation entity in accommodationService.GetAll())
            {
                if (entity.Type.ToString() == selectedType)
                {
                    MyListBox.Items.Add(entity.Id + " " + entity.Name + " " + entity.Location.City + " " + entity.Location.Country + " " + entity.GuestLimit + " " + entity.MinimalStay + " " + entity.CancelationLimit);
                }

            }
        }


        private void CityComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            MyListBox.Items.Clear();

            string selectedType = TypesComboBox.SelectedItem.ToString();
            string selectedCity = CityComboBox.SelectedItem.ToString();
            AccommodationService accommodationService = new AccommodationService();
            foreach (Accommodation entity in accommodationService.GetAll())
            {
                if (entity.Location.City.ToString() == selectedCity && entity.Type.ToString() == selectedType)
                {
                    MyListBox.Items.Add(entity.Id + " " + entity.Name + " " + entity.Location.City + " " + entity.Location.Country + " " + entity.GuestLimit + " " + entity.MinimalStay + " " + entity.CancelationLimit);
                }

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AccommodationSearchView win2 = new AccommodationSearchView();
            win2.Show();
        }
    }
}
