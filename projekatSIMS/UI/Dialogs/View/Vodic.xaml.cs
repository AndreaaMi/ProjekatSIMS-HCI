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
            KeyPointsService keyPointsService = new KeyPointsService();
            Tour newTour = new Tour();
            newTour.Id = int.Parse(IdBox.Text);
            newTour.Name = NameBox.Text;
            newTour.Location = new Location {Country = CountryBox.Text ,City = CityBox.Text  };
            if (Enum.TryParse(LanguageBox.Text.ToString(), out Language type))
            {
                newTour.Language = type;
            
            }
            
            newTour.StartingDate = DateTime.Parse(DateBox.Text);
            newTour.StartingTime = TimeBox.Text;
            newTour.MaxNumberOfGuests = int.Parse(GuestNumBox.Text);
            newTour.Duration = int.Parse(DurationBox.Text);
            newTour.Description = DescriptionBox.Text;
            newTour.GuestNumber = 22;
            
           //////
                string i = KeyPointsIdBox.Text;
                string j = KeyPointsNameBox.Text;
                string[] keyId = i.Split(' '); 
                string[] keyName = j.Split(' ');
                int k = 0;




            foreach (var word in keyId)
                    {
                        
                        
                            if(word == "x")
                            { 
                                break; 
                            }
                         KeyPoints key = new KeyPoints();
                         key.Id = int.Parse(word);
                         key.Name = keyName[k];
                         key.IsActive = false;
                         key.AssociatedTour = int.Parse(IdBox.Text);
                         newTour.KeyPoints.Add(key);
                         keyPointsService.Add(key);
                         k++;
                    }

                    
                
           //////
           ///
            tourService.Add(newTour);
            foreach(Tour entity in tourService.GetAll())
            {
                List1.Items.Add(entity.Id + "|" + entity.Name + "|" + entity.Location.Country + "|" + entity.Location.City + "|" + entity.Language + "|" + entity.StartingDate + "|" + entity.StartingTime + "|" + entity.MaxNumberOfGuests + "|" + entity.Duration + "|" + entity.Description);
            }
            // Dodajte novi element u ListBox
            

            // Očistite TextBox-e za sljedeći unos
            IdBox.Clear();
            NameBox.Clear();
            CountryBox.Clear();
            CityBox.Clear();
            LanguageBox.Clear();
            DateBox.Clear();
            TimeBox.Clear();
            GuestNumBox.Clear();
            DurationBox.Clear();
            DescriptionBox.Clear();
            KeyPointsIdBox.Clear();
            DescriptionBox.Clear();
            
            
        }
        
        private void listToursToday_Click(object sender, RoutedEventArgs e)
        {
            TourService tourService = new TourService();
            KeyPointsService keyPointsService = new KeyPointsService();

            foreach (Tour entity in tourService.GetAll())
            {
                if (entity.StartingDate == DateTime.Parse("11.11.2011"))
                {
                    List1.Items.Add(entity.Id + "|" + entity.Name);
                }
            }



        }

        private void listPoints_Click(object sender, RoutedEventArgs e)
        {
            TourService tourService = new TourService();
            KeyPointsService keyPointsService = new KeyPointsService();
            
                    foreach(KeyPoints kp in keyPointsService.GetAll()) 
                    {
                        if(kp.AssociatedTour ==  1)
                        {
                            List2.Items.Add(kp.Id + "|" + kp.Name);
                             if (kp.Id == 1 && kp.AssociatedTour == 1)
                             {
                                 kp.IsActive = true;
                                 keyPointsService.Edit(kp);
                             }
                        }
                    }
                
            
        }

        private void EndTour_Click(object sender, RoutedEventArgs e)
        {
            TourService tourService = new TourService();
            KeyPointsService keyPointsService = new KeyPointsService();

            foreach (KeyPoints kp in keyPointsService.GetAll())
            {
                if (kp.AssociatedTour == 1)
                {
                    
                    if (kp.Id == 1 && kp.AssociatedTour == 1)
                    {
                        kp.IsActive = true;
                        keyPointsService.Edit(kp);
                    }
                }
            }

        }

        private void KpSelect_Click(object sender, RoutedEventArgs e)
        {
            TourService tourService = new TourService();
            KeyPointsService keyPointsService = new KeyPointsService();

            foreach (KeyPoints kp in keyPointsService.GetAll())
            {
                
                    
                    if (kp.Id == 6 && kp.AssociatedTour == 11)
                    {
                        kp.IsActive = true;
                        keyPointsService.Edit(kp);
                    }
                
            }
        }
    }
}
