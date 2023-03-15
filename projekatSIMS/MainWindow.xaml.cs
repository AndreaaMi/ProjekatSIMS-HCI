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
            AccommodationService accommodationService = new AccommodationService();
            foreach (Accommodation entity in accommodationService.GetAll())
            {
                TypesComboBox.Items.Add(entity.Type);
                CityComboBox.Items.Add(entity.Location.City);

            }

            RemoveDuplicateItemsFromComboBox(TypesComboBox);
            RemoveDuplicateItemsFromComboBox(CityComboBox);
        }
    
        private void RemoveDuplicateItemsFromComboBox(ComboBox comboBox)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                for (int j = i + 1; j < comboBox.Items.Count; j++)
                {
                    if (comboBox.Items[i].Equals(comboBox.Items[j]))
                    {
                        comboBox.Items.RemoveAt(j);
                        j--;
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TourService tourService = new TourService();
            foreach(Tour t in tourService.GetAll())
            {
                Debug.WriteLine(t.Id + " " + t.Name + " " + t.Location.Country + " " + t.Location.City + " " + t.Language.ToString() + " " + t.StartingDate.ToString("dd/MM/yyyy") + " " + t.StartingTime + " " + t.Duration + " " + t.MaxNumberOfGuests );
            }
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
    }
}
