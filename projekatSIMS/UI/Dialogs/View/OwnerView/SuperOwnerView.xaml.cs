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



namespace projekatSIMS.UI.Dialogs.View.OwnerView
{
    /// <summary>
    /// Interaction logic for SuperOwnerView.xaml
    /// </summary>
    public partial class SuperOwnerView : Window
    {
        public SuperOwnerView()
        {
            InitializeComponent();
            List<Accommodation> accommodations = new List<Accommodation>();
            AccommodationService accommodationService = new AccommodationService();
            foreach (Accommodation a in accommodationService.GetAll())
            {
                accommodations.Add(a);
            }
            this.AccommodationImageListBox.ItemsSource = accommodations;
            this.AccommodationImageListBox.DisplayMemberPath = "Name";


        }

        private void AccommodationImageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedItem = AccommodationImageListBox.SelectedItem as Accommodation;

            if (selectedItem != null)
            {
                // Prikazivanje informacija o selektovanom itemu u TextBox-ovima
                NameTextBox.Text = selectedItem.Name;
                CityTextBox.Text = selectedItem.Location.City;
                CountryTextBox.Text = selectedItem.Location.Country;
                TypeTextBox.Text = selectedItem.Type.ToString();
                ImageUrlTextBox.Text = selectedItem.ImageUrls[0];

                /* BitmapImage bitmapImage = new BitmapImage();
                 bitmapImage.BeginInit();
                 bitmapImage.UriSource = new Uri(ImageUrlTextBox.Text);
                 bitmapImage.EndInit();

                 // Postavljanje izvora slike na Image kontrolu
                 imgDisplay.DataContext = bitmapImage;*/


            }

        }

        private void AccommodationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void AccommodationShowButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationListBox.Items.Clear();
            AccommodationService accommodationService = new AccommodationService();
            UserService userService = new UserService();

            List<string> superOwnerAccommodations = new List<string>(); // Lista za smeštaje čiji je vlasnik super vlasnik
            List<string> otherAccommodations = new List<string>(); // Lista za smeštaje čiji vlasnik nije super vlasnik

            foreach (Accommodation entity in accommodationService.GetAll())
            {
                User user = (User)userService.Get(entity.OwnerId);
                //string item = entity.Name + " " + entity.OwnerId + " " + entity.Location.City + " " + entity.Location.Country + " " + entity.Type;
                if (user.SuperStatus == true)
                {
                    string item = "*SUPER*" + entity.Name + " " + entity.OwnerId + " " + entity.Location.City + " " + entity.Location.Country + " " + entity.Type;

                    superOwnerAccommodations.Add(item);
                }
                else
                {
                    string item = entity.Name + " " + entity.OwnerId + " " + entity.Location.City + " " + entity.Location.Country + " " + entity.Type;

                    otherAccommodations.Add(item);
                }
            }

            // Dodajte smeštaje čiji je vlasnik super vlasnik prvo
            foreach (string item in superOwnerAccommodations)
            {
                AccommodationListBox.Items.Add(item);
            }

            // Dodajte smeštaje čiji vlasnik nije super vlasnik nakon toga
            foreach (string item in otherAccommodations)
            {
                AccommodationListBox.Items.Add(item);
            }
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TypeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
