﻿using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.TourGuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace projekatSIMS.UI.Dialogs.ViewModel.TourGuideViewModel
{
    internal class TourGuideStartTourPageModel : ViewModelBase
    {
        #region SIDE BAR
        private RelayCommand profilePageCommand;
        private RelayCommand homePageCommand;
        private RelayCommand newTourPageCommand;
        private RelayCommand allToursPageCommand;
        private RelayCommand toursTodayPageCommand;
        private RelayCommand tourRequestsPageCommand;

        private void ProfilePageCommandExecute()
        {

            TourGuideMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TourGuideView/TourGuideProfilePageView.xaml", UriKind.Relative));


        }

        private void HomePageCommandExecute()
        {

            TourGuideMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TourGuideView/TourGuideHomePageView.xaml", UriKind.Relative));


        }

        private void NewTourPageCommandExecute()
        {

            TourGuideMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TourGuideView/TourGuideNewTourPageView.xaml", UriKind.Relative));


        }

        private void AllToursPageCommandExecute()
        {

            TourGuideMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TourGuideView/TourGuideAllToursPageView.xaml", UriKind.Relative));


        }

        private void ToursTodayPageCommandExecute()
        {

            TourGuideMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TourGuideView/TourGuideToursTodayPageView.xaml", UriKind.Relative));


        }

        private void TourRequestsPageCommandExecute()
        {

            TourGuideMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TourGuideView/TourGuideTourRequestsPageView.xaml", UriKind.Relative));


        }
        public RelayCommand ProfilePageCommand
        {
            get
            {
                if (profilePageCommand == null)
                {
                    profilePageCommand = new RelayCommand(param => ProfilePageCommandExecute());
                }

                return profilePageCommand;
            }
        }

        public RelayCommand HomePageCommand
        {
            get
            {
                if (homePageCommand == null)
                {
                    homePageCommand = new RelayCommand(param => HomePageCommandExecute());
                }

                return homePageCommand;
            }
        }

        public RelayCommand NewTourPageCommand
        {
            get
            {
                if (newTourPageCommand == null)
                {
                    newTourPageCommand = new RelayCommand(param => NewTourPageCommandExecute());
                }

                return newTourPageCommand;
            }
        }

        public RelayCommand AllToursPageCommand
        {
            get
            {
                if (allToursPageCommand == null)
                {
                    allToursPageCommand = new RelayCommand(param => AllToursPageCommandExecute());
                }

                return allToursPageCommand;
            }
        }

        public RelayCommand ToursTodayPageCommand
        {
            get
            {
                if (toursTodayPageCommand == null)
                {
                    toursTodayPageCommand = new RelayCommand(param => ToursTodayPageCommandExecute());
                }

                return toursTodayPageCommand;
            }
        }

        public RelayCommand TourRequestsPageCommand
        {
            get
            {
                if (tourRequestsPageCommand == null)
                {
                    tourRequestsPageCommand = new RelayCommand(param => TourRequestsPageCommandExecute());
                }

                return tourRequestsPageCommand;
            }
        }


        #endregion

        private string id;
        private string name;
        private string country;
        private string city;
        private DateTime startingDate;
        private string time;
        private string maxNumberOfGuests;
        private string duration;
        private string description;
        private string keyPointId;
        private string keyPointName;
        private int associatedTour;
        private List<KeyPoints> keyPoints;

        private KeyPoints selectedKey = new KeyPoints();
        

        private ObservableCollection<Tour> tours = new ObservableCollection<Tour>();
        private ObservableCollection<KeyPoints> keys = new ObservableCollection<KeyPoints>();

        private TourService tourService;
        private KeyPointsService keyPointsService;
        private TourReservationService tourReservationService;
        private VoucherService voucherService;
        private UserService userService;

        private RelayCommand tourButtonCommand;
        private RelayCommand endTourCommand;


        public TourGuideStartTourPageModel(Tour selected)
        {
            SetService();
            var tours = tourService.GetAll();
            var key = keyPointsService.GetAll();

            foreach(KeyPoints keyy in key)
            {
                if(keyy.AssociatedTour == selected.Id)
                {
                    Keys.Add(keyy);
                }
            }

             
            Tours.Add(selected);
        }

        public void SetService()
        {
            tourService = new TourService();
            keyPointsService = new KeyPointsService();
            tourReservationService = new TourReservationService();
            voucherService = new VoucherService();
            userService = new UserService();
        }

        private void TourButtonCommandExecute()
        {
              if (selectedKey != null)
               {
                   
                var key = keyPointsService.GetAll();
                foreach (KeyPoints keyy in key)
                {
                    if (keyy.Id == selectedKey.Id)
                    {
                        keyy.IsActive = true;
                        keyPointsService.Edit(keyy);
                    }
                }

            }
              else
              {
                 MessageBox.Show("eeeeeeeeeeeee");
              }
        }

        private void EndTourCommandExecute()
        {
            if (selectedKey != null)
            {

                var key = keyPointsService.GetAll();
                foreach (KeyPoints keyy in key)
                {
                    if (keyy.AssociatedTour == selectedKey.AssociatedTour)
                    {
                        keyy.IsActive = true;
                        keyPointsService.Edit(keyy);
                    }
                }

            }
            else
            {
                MessageBox.Show("eeeeeeeeeeeee");
            }
        }

        public RelayCommand EndTourCommand
        {
            get
            {
                if (endTourCommand == null)
                {
                    endTourCommand = new RelayCommand(param => EndTourCommandExecute());
                }

                return endTourCommand;
            }
        }

        public RelayCommand TourButtonCommand
        {
            get
            {
                if (tourButtonCommand == null)
                {
                    tourButtonCommand = new RelayCommand(param => TourButtonCommandExecute());
                }

                return tourButtonCommand;
            }
        }

        public KeyPoints SelectedKey
        {
            get { return selectedKey; }
            set
            {
                selectedKey = value;
                OnPropertyChanged(nameof(SelectedKey));
            }

        }
        public ObservableCollection<Tour> Tours
        {
            get { return tours; }
            set
            {
                tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

        public ObservableCollection<KeyPoints> Keys
        {
            get { return keys; }
            set
            {
                keys = value;
                OnPropertyChanged(nameof(Keys));
            }
        }

        public int AssociatedTour
        {
            get { return associatedTour; }
            set
            {
                associatedTour = value;
                OnPropertyChanged(nameof(AssociatedTour));

            }
        }
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));

            }
        }




        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }


        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }


        public DateTime StartingDate
        {
            get { return startingDate; }
            set
            {
                startingDate = value;
                OnPropertyChanged(nameof(StartingDate));
            }
        }


        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged(nameof(Time));
            }
        }


        public string MaximumNumberOfGuests
        {
            get { return maxNumberOfGuests; }
            set
            {
                maxNumberOfGuests = value;
                OnPropertyChanged(nameof(MaximumNumberOfGuests));
            }
        }


        public string Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }


        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }


        public string KeyPointId
        {
            get { return keyPointId; }
            set
            {
                keyPointId = value;
                OnPropertyChanged(nameof(KeyPointId));
            }
        }


        public string KeyPointName
        {
            get { return keyPointName; }
            set
            {
                keyPointName = value;
                OnPropertyChanged(nameof(KeyPointName));
            }
        }

        public List<KeyPoints> KeyPoints
        {
            get { return keyPoints; }
            set
            {
                keyPoints = value;
                OnPropertyChanged(nameof(KeyPoints));
            }


        }
    }
}
