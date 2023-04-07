using projekatSIMS.Model;
using projekatSIMS.Service;
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
    /// Interaction logic for Tourist.xaml
    /// </summary>
    public partial class Tourist : Window
    {
        public Tourist()
        {
            InitializeComponent();
            TourService tourService = new TourService();
            var tours = tourService.GetAll();
            CountryComboBox.ItemsSource = tours.Where(a => a is Tour).Select(a => ((Tour)a).Location.country).Distinct();
            CityComboBox.ItemsSource = tours.Where(a => a is Tour).Select(a => ((Tour)a).Location.city).Distinct();
            DurationComboBox.ItemsSource = tours.Where(a => a is Tour).Select(a => ((Tour)a).Duration).Distinct();
            LanguageComboBox.ItemsSource =  tours.Where(a => a is Tour).Select(a => ((Tour)a).Language.ToString()).Distinct();

        }

        private void DisplayTours_Click(object sender, RoutedEventArgs e)
        {
            MyListBox.Items.Clear();

            TourService tourService = new TourService();

            foreach (Tour entity in tourService.GetAll())
            {
                MyListBox.Items.Add(entity.Id + " " + entity.Name + " " + entity.Location.Country + " " + entity.Location.City + " " + entity.Language.ToString() + " " + entity.StartingDate.ToString("dd/MM/yyyy") + " " + entity.StartingTime.ToString() + " " + entity.MaxNumberOfGuests + " " + entity.Duration);
            }
        }

        private void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyListBox.Items.Clear();
            string selectedType = CountryComboBox.SelectedItem.ToString();
            TourService tourService = new TourService();
            foreach (Tour entity in tourService.GetAll())
            {
                if (entity.Location.country.ToString() == selectedType)
                {
                    MyListBox.Items.Add(entity.Id + " " + entity.Name + " " + entity.Location.Country + " " + entity.Location.City + " " + entity.Language.ToString() + " " + entity.StartingDate.ToString("dd/MM/yyyy") + " " + entity.StartingTime.ToString() + " " + entity.MaxNumberOfGuests + " " + entity.Duration);
                }

            }
        }


        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyListBox.Items.Clear();
            string selectedCity = CityComboBox.SelectedItem.ToString();
            TourService tourService = new TourService();
            foreach (Tour entity in tourService.GetAll())
            {
                if (entity.Location.city.ToString() == selectedCity)
                {
                    MyListBox.Items.Add(entity.Id + " " + entity.Name + " " + entity.Location.Country + " " + entity.Location.City + " " + entity.Language.ToString() + " " + entity.StartingDate.ToString("dd/MM/yyyy") + " " + entity.StartingTime.ToString() + " " + entity.MaxNumberOfGuests + " " + entity.Duration);
                }

            }
        }

        private void DurationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyListBox.Items.Clear();
            string selectedDuration = DurationComboBox.SelectedItem.ToString();
            TourService tourService = new TourService();
            foreach (Tour entity in tourService.GetAll())
            {
                if (entity.Duration.ToString() == selectedDuration)
                {
                    MyListBox.Items.Add(entity.Id + " " + entity.Name + " " + entity.Location.Country + " " + entity.Location.City + " " + entity.Language.ToString() + " " + entity.StartingDate.ToString("dd/MM/yyyy") + " " + entity.StartingTime.ToString() + " " + entity.MaxNumberOfGuests + " " + entity.Duration);
                }

            }
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyListBox.Items.Clear();
            string selectedLanguage = LanguageComboBox.SelectedItem.ToString();
            TourService tourService = new TourService();
            foreach (Tour entity in tourService.GetAll())
            {
                if (entity.Language.ToString() == selectedLanguage)
                {
                    MyListBox.Items.Add(entity.Id + " " + entity.Name + " " + entity.Location.Country + " " + entity.Location.City + " " + entity.Language.ToString() + " " + entity.StartingDate.ToString("dd/MM/yyyy") + " " + entity.StartingTime.ToString() + " " + entity.MaxNumberOfGuests + " " + entity.Duration);
                }

            }
        }
        
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            MyListBox.Items.Clear();
            int guests = int.Parse(NumberOfGuests.Text);
            TourService tourService = new TourService();
            foreach (Tour entity in tourService.GetAll())
            {
                if (entity.MaxNumberOfGuests >= guests)
                {
                    MyListBox.Items.Add(entity.Id + " " + entity.Name + " " + entity.Location.Country + " " + entity.Location.City + " " + entity.Language.ToString() + " " + entity.StartingDate.ToString("dd/MM/yyyy") + " " + entity.StartingTime.ToString() + " " + entity.MaxNumberOfGuests + " " + entity.Duration);
                }

            }
        }

        private void Reservations_Click(object sender, RoutedEventArgs e)
        {
            TourReservationView win = new TourReservationView();
            win.Show();
        }
    }
}
