using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.GuestView;
using projekatSIMS.UI.Dialogs.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
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
            PopulateComboBoxes();
            NameComboBox.PreviewTextInput += NameComboBox_PreviewTextInput;
        }
        private void TypesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedType = TypesComboBox.SelectedItem.ToString();
            PopulateListBoxByType(selectedType);
        }

        private void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCountry = CountryComboBox.SelectedItem.ToString();
            PopulateListBoxByCountry(selectedCountry);
        }

        private void NameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NameComboBox.SelectedItem != null){
                PopulateListBoxByName(NameComboBox.SelectedItem.ToString());
            }
            else{
                MessageBox.Show("No Matching Names");
            }
        }

        private void NameComboBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var searchText = comboBox.Text + e.Text;

            var matchingItem = comboBox.Items.Cast<string>().FirstOrDefault(item =>
            {
                var words = item.Split(' ');
                return words.Any(word => word.StartsWith(searchText, StringComparison.OrdinalIgnoreCase));
            });

            if (matchingItem != null){
                comboBox.SelectedItem = matchingItem;
                PopulateListBoxByName(comboBox.SelectedItem.ToString());
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearList();

            var selectedType = TypesComboBox.SelectedItem.ToString();
            var selectedCity = CityComboBox.SelectedItem.ToString();

            AccommodationService accommodationService = new AccommodationService();

            // Adds each matching accommodation to the list box
            foreach (Accommodation entity in accommodationService.GetAll()){
                if (entity.Location.City == selectedCity && entity.Type.ToString() == selectedType){
                    AddItemToListBox(entity);
                }
            }
            // Set the selected value of the CityComboBox to the newly selected city
            CityComboBox.SelectedValue = selectedCity;
        }

          private void DisplayByMinimalStayButton_Click(object sender, RoutedEventArgs e)
          {
              ClearList();

              string resDays = MinimalStatTextBox.Text.ToString();
              int resDaysInt = int.Parse(resDays);

              AccommodationService accommodationService = new AccommodationService();

              foreach (Accommodation entity in accommodationService.GetAll().Cast<Accommodation>())
              {
                  if (entity.MinimumStayDays.ToString() == resDays || entity.MinimumStayDays < resDaysInt){
                      AddItemToListBox(entity);
                  }
              }

              if (IsEmpty(MyListBox)){
                  MessageBox.Show("Invalid Minimal Stay days! You must enter a larger number.", "Numeric error", MessageBoxButton.OK, MessageBoxImage.Error);
              }

              MinimalStatTextBox.Clear();
          }

        // Checks if list box is empty
        public bool IsEmpty(ListBox listBox){
            return listBox.Items.Count <= 0;
        }

        private void ClearList(){
            MyListBox.Items.Clear();
        }

        private void PopulateComboBoxes()
        {
            var accommodationService = new AccommodationService();
            var accommodations = accommodationService.GetAll().OfType<Accommodation>();

            NameComboBox.ItemsSource = accommodations.Select(a => a.Name).Distinct();
            TypesComboBox.ItemsSource = accommodations.Select(a => a.Type).Distinct();
            CityComboBox.ItemsSource = accommodations.Where(a => a.Location != null).Select(a => a.Location.City).Distinct();
            CountryComboBox.ItemsSource = accommodations.Where(a => a.Location != null).Select(a => a.Location.Country).Distinct();
        }

        private void DisplayAccommodations_Click(object sender, RoutedEventArgs e)
        {
            PopulateListBox();
        }

        private void AddItemToListBox(Accommodation entity)
        {
            MyListBox.Items.Add($"{entity.Id} {entity.Name} {entity.Location.City} {entity.Location.Country} {entity.GuestLimit} {entity.MinimumStayDays} {entity.CancellationDays}");
        }
        // Populates the list box with all accommodations
        private void PopulateListBox()
          {
              ClearList();
              AccommodationService accommodationService = new AccommodationService();
              foreach (Accommodation entity in accommodationService.GetAll().Cast<Accommodation>())
              {
                  AddItemToListBox(entity);
              }
          }

          // Populates the list box with accommodations of a certain type
          private void PopulateListBoxByType(string selected)
          {
              ClearList();
              AccommodationService accommodationService = new AccommodationService();
              foreach (Accommodation entity in accommodationService.GetAll().OfType<Accommodation>())
              {
                  if (entity.Type.ToString() == selected)
                  {
                      AddItemToListBox(entity);
                  }
              }
          }

          // Populates the list box with accommodations that have a certain name
          private void PopulateListBoxByName(string selected)
          {
              ClearList();
              AccommodationService accommodationService = new AccommodationService();
              foreach (Accommodation entity in accommodationService.GetAll().OfType<Accommodation>())
              { 
                  if (entity.Name.ToString() == selected)
                  {
                      AddItemToListBox(entity);
                  }
              }
          }

          // Populates the list box with accommodations in a certain country
          private void PopulateListBoxByCountry(string selected)
          {
              ClearList();
              AccommodationService accommodationService = new AccommodationService();
              foreach (Accommodation entity in accommodationService.GetAll().OfType<Accommodation>())
              {
                  if (entity.Location.Country.ToString() == selected)
                  {
                      AddItemToListBox(entity);
                  }
              }
          }

          private void SearchByGuestNumber_Click(object sender, RoutedEventArgs e)
          {
            ClearList();
            string guestNumber = GuestNumberTextBox.Text.ToString();

              try
              {
                  int guestNumberInt = int.Parse(guestNumber);

                  AccommodationService accommodationService = new AccommodationService();
                  foreach (Accommodation entity in accommodationService.GetAll().Cast<Accommodation>())
                  {
                      if (entity.GuestLimit.ToString() == guestNumber || entity.GuestLimit > guestNumberInt){
                          AddItemToListBox(entity);
                      }
                  }

                  if (IsEmpty(MyListBox)){
                      MessageBox.Show("Invalid number of guests! You must enter a smaller number.", "Numeric error", MessageBoxButton.OK, MessageBoxImage.Error);
                  }

                  GuestNumberTextBox.Clear();
              }
              catch (FormatException){
                  MessageBox.Show("Invalid input! Please enter a valid number.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
              }
          }
        private void GoToToReservationButton_Click(object sender, RoutedEventArgs e){
            AccommodationReservationView win = new AccommodationReservationView();
            win.Show();
        }

        private void GoToMainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            GuestMainView win = new GuestMainView();
            win.Show();
        }
    }
}
