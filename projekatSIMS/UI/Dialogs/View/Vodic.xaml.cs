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
    /// Interaction logic for Vodic.xaml
    /// </summary>
    public partial class Vodic : Window
    {
        public Vodic()
        {
            InitializeComponent();
        }

        private void EnterParamsButton_Click(object sender, RoutedEventArgs e)
        {
            
            
            TourService tourService = new TourService();
            Tour newTour = new Tour();
            newTour.Id = 1;
            newTour.Name = NameBox.Text;
            newTour.Location.Country = CountryBox.Text;
            newTour.Location.City = CityBox.Text;
            //newTour.Language = ;
            newTour.StartingDate = DateTime.Parse(DateBox.Text);
            newTour.StartingTime = TimeBox.Text;
            newTour.MaxNumberOfGuests = int.Parse(GuestNumBox.Text);
            newTour.Duration = int.Parse(DurationBox.Text);

            tourService.Add(newTour);
            foreach(Tour entity in tourService.GetAll())
            {
                List1.Items.Add(entity.Id + "|" + entity.Name + "|" + entity.Location.Country + "|" + entity.Location.City + "|" + entity.Language + "|" + entity.StartingDate + "|" + entity.StartingTime + "|" + entity.MaxNumberOfGuests + "|" + entity.Duration);
            }
            // Dodajte novi element u ListBox
            

            // Očistite TextBox-e za sljedeći unos
            NameBox.Clear();
            CountryBox.Clear();
            CityBox.Clear();
            LanguageBox.Clear();
            DateBox.Clear();
            TimeBox.Clear();
            GuestNumBox.Clear();
            DurationBox.Clear();
            DescriptionBox.Clear();
            KeyPointsBox.Clear();

        }
    }
}
