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
    /// Interaction logic for AccommodationRegistrationView.xaml
    /// </summary>
    public partial class AccommodationRegistrationView : Window
    {
        public AccommodationRegistrationView()
        {
            InitializeComponent();
            AccommodationService accommodationService = new AccommodationService();
            var accommodations = accommodationService.GetAll();
            TypeComboBox.ItemsSource = accommodations.Where(a => a is Accommodation).Select(a => ((Accommodation)a).Type).Distinct();

        }





        private void IdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GuestLimitTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MinimalStayTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CancelationLimitTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AllAccommodationsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            AllAccommodationsListBox.Items.Clear();

            AccommodationService accommodationService = new AccommodationService();

             

            // Extract the values from the text boxes and use them to create a new Accommodation object
            Accommodation newAccommodation = new Accommodation();

            //newAccommodation.Id = int.Parse(IdTextBox.Text);
            newAccommodation.Name = NameTextBox.Text;
            newAccommodation.Location = new Location { City = CityTextBox.Text, Country = CountryTextBox.Text };
            if (Enum.TryParse(TypeComboBox.SelectedItem.ToString(), out AccommodationType type))
            {
                newAccommodation.Type = type;

            }
            newAccommodation.GuestLimit = int.Parse(GuestLimitTextBox.Text);
            
            newAccommodation.MinimumStayDays = int.Parse(MinimalStayTextBox.Text);
            newAccommodation.CancellationDays = int.Parse(CancelationLimitTextBox.Text);

            // Save the new Accommodation object to the database using the AccommodationService
            accommodationService.RegisterAccommodation(newAccommodation);
           
            
            foreach (Accommodation entity in accommodationService.GetAll())
            {
                AllAccommodationsListBox.Items.Add(entity.Id + " " + entity.Name + " " + entity.Location.City + " " + entity.Location.Country + " " + entity.Type + " " + entity.GuestLimit + " " + entity.MinimumStayDays + " " + entity.CancellationDays);
            }
        }

        private void DeleteIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            AllAccommodationsListBox.Items.Clear();

            AccommodationService accommodationService = new AccommodationService();
            Accommodation accommodation = new Accommodation();

            foreach (Accommodation entity in accommodationService.GetAll())
            {
                if(entity.Id == int.Parse(DeleteIdTextBox.Text))
                { 
                    accommodationService.Remove(entity);
                    break;
                }

            }

            foreach (Accommodation entity in accommodationService.GetAll())
            {
                AllAccommodationsListBox.Items.Add(entity.Id + " " + entity.Name + " " + entity.Location.City  + " " + entity.Location.Country + " " + entity.Type + " " + entity.GuestLimit + " " + entity.MinimumStayDays + " " + entity.CancellationDays);
            }



        }

        private void CountryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GuestReviewButton_Click(object sender, RoutedEventArgs e)
        {
            GuestReviewView win2 = new GuestReviewView();
            win2.Show();

        }
    }
}
