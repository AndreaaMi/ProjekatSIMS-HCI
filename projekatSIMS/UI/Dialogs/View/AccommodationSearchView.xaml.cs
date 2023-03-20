using projekatSIMS.Model;
using projekatSIMS.Service;
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
        private Accommodation selectedAccommodation;
        private static volatile AccommodationSearchView instance;
        public AccommodationSearchView()
        {
            InitializeComponent();
            PopulateComboBoxes();
            NameComboBox.PreviewTextInput += NameComboBox_PreviewTextInput;
        }

          private void DisplayAccommodations_Click(object sender, RoutedEventArgs e)
          {
              PopulateListBox();
          }

          // Handles text input in the name combo box and filters accommodations by name
          private void NameComboBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
          {
              var comboBox = sender as ComboBox;
              var searchText = comboBox.Text + e.Text;

              var itemIndex = comboBox.Items.IndexOf(searchText);
              if (itemIndex >= 0)
              {
                  comboBox.SelectedItem = comboBox.Items[itemIndex];
                  PopulateListBoxByName(comboBox.SelectedItem.ToString());
              }
          }
          // Handles selection changes in the name combo box and filters accommodations by name
          private void NameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
              if (NameComboBox.SelectedItem != null)
              {
                  PopulateListBoxByName(NameComboBox.SelectedItem.ToString());
              }
              else
              {
                  MessageBox.Show("No Matching Names");
              }
          }

          // Handles selection changes in the combo boxes and filters accommodations by selectedtypes
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

          private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
              // Clears the list box before adding new items
              MyListBox.Items.Clear();

              var selectedType = TypesComboBox.SelectedItem.ToString();
              var selectedCity = CityComboBox.SelectedItem.ToString();

              var accommodationService = new AccommodationService();

              // Adds each matching accommodation to the list box
              foreach (Accommodation entity in accommodationService.GetAll().Cast<Accommodation>())
              {
                  if (entity.Location?.City.ToString() == selectedCity && entity is Accommodation accommodation && accommodation.Type.ToString() == selectedType)
                  {
                      AddItemToListBox(entity);
                  }
              }
          }


          // Populates all the comboboxes with the distinct values of accommodation types, city names, country names,
          private void PopulateComboBoxes()
          {
              var accommodationService = new AccommodationService();
              var accommodations = accommodationService.GetAll().OfType<Accommodation>();

              TypesComboBox.ItemsSource = accommodations.Select(a => a.Type).Distinct();
              CityComboBox.ItemsSource = accommodations.Where(a => a.Location != null).Select(a => a.Location.City).Distinct();
              CountryComboBox.ItemsSource = accommodations.Where(a => a.Location != null).Select(a => a.Location.Country).Distinct();
              NameComboBox.ItemsSource = accommodations.Select(a => a.Name).Distinct();
          }

          // Displays accommodations by minimal stay
          private void DisplayByMinimalStayButton_Click(object sender, RoutedEventArgs e)
          {
              MyListBox.Items.Clear();

              string resDays = MinimalStatTextBox.Text.ToString();

              int resDaysInt = int.Parse(resDays);

              AccommodationService accommodationService = new AccommodationService();

              foreach (Accommodation entity in accommodationService.GetAll().Cast<Accommodation>())
              {
                  if (entity.MinimalStay.ToString() == resDays || entity.MinimalStay < resDaysInt)
                  {
                      AddItemToListBox(entity);
                  }
              }

              if (IsEmpty(MyListBox))
              {
                  MessageBox.Show("Invalid Minimal Stay days! You must enter a larger number.", "Numeric error", MessageBoxButton.OK, MessageBoxImage.Error);
              }

              MinimalStatTextBox.Clear();
          }

          // Checks if list box is empty
          public bool IsEmpty(ListBox listBox)
          {
              return listBox.Items.Count <= 0;
          }
        
          // Populates the list box with all accommodations
          private void PopulateListBox()
          {
              MyListBox.Items.Clear();
              AccommodationService accommodationService = new AccommodationService();

              foreach (Accommodation entity in accommodationService.GetAll().Cast<Accommodation>())
              {
                  AddItemToListBox(entity);
              }
          }

          // Populates the list box with accommodations of a certain type
          private void PopulateListBoxByType(string selected)
          {
              MyListBox.Items.Clear();
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
              MyListBox.Items.Clear();
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
              MyListBox.Items.Clear();
              AccommodationService accommodationService = new AccommodationService();

              foreach (Accommodation entity in accommodationService.GetAll().OfType<Accommodation>())
              {
                  if (entity.Location.Country.ToString() == selected)
                  {
                      AddItemToListBox(entity);
                  }
              }
          }

          // Populates the list box with accommodations that have a certain guest limit
          private void PopulateListBoxByGuestLimit(string selected)
          {
              MyListBox.Items.Clear();
              AccommodationService accommodationService = new AccommodationService();

              foreach (Accommodation entity in accommodationService.GetAll().OfType<Accommodation>())
              {
                  if (entity.GuestLimit.ToString() == selected)
                  {
                      AddItemToListBox(entity);
                  }
              }
          }

          // Adds an item to the list box with information about an accommodation
          private void AddItemToListBox(Accommodation entity)
          {
              MyListBox.Items.Add($"{entity.Id} {entity.Name} {entity.Location.City} {entity.Location.Country} {entity.GuestLimit} {entity.MinimalStay} {entity.CancelationLimit}");
          }

          private void SearchByGuestNumber_Click(object sender, RoutedEventArgs e)
          {
              MyListBox.Items.Clear();

              string guestNumber = GuestNumberTextBox.Text.ToString();

              try
              {
                  int guestNumberInt = int.Parse(guestNumber);

                  AccommodationService accommodationService = new AccommodationService();

                  foreach (Accommodation entity in accommodationService.GetAll().Cast<Accommodation>())
                  {
                      if (entity.GuestLimit.ToString() == guestNumber || entity.GuestLimit > guestNumberInt)
                      {
                          AddItemToListBox(entity);
                      }
                  }

                  if (IsEmpty(MyListBox))
                  {
                      MessageBox.Show("Invalid number of guests! You must enter a smaller number.", "Numeric error", MessageBoxButton.OK, MessageBoxImage.Error);
                  }

                  GuestNumberTextBox.Clear();
              }
              catch (FormatException)
              {
                  MessageBox.Show("Invalid input! Please enter a valid number.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
              }
          }

          private void MyListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {

          }

        private void GoToToReservationButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationView win = new AccommodationReservationView();
            win.Show();
        }
    }
}
