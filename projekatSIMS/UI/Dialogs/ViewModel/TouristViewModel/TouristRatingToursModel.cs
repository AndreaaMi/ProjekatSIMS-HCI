using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristRatingToursModel : ViewModelBase
    {
        private RelayCommand backCommand;
        private RelayCommand submitCommand;

        private ObservableCollection<Tour> items = new ObservableCollection<Tour>();

        private int touristId;
        private int tourGuideKnowledge;
        private int tourGuideLanguageProficiency;
        private int interestLevel;
        private string comment;
        private string imageUrl;

        private TourService tourService;
        private KeyPointsService keyPointsService;
        private TourReservationService reservationService;
        private UserService userService;
        private TourRatingService ratingService;
        public TouristRatingToursModel()
        {
            SetService(); 
            LoadData();

        }

        #region LOADING IN THE DATA
        //FIRST WE LOAD IN ALL THE TOURS THAT THE GUEST HAS A RESERVATION FOR

        private void LoadData()
        {
            InitialDataGridLoad();
            FilterDataGrid();
        }
        private void InitialDataGridLoad()
        {
            foreach(Tour tour in tourService.GetAll())
            {
                if(reservationService.GetReservationByGuestId(userService.GetLoginUser().Id).Contains(tour.Id))
                {
                    Items.Add(tour);
                }
            }
        }

        //THEN WE FILTER OUT THE ONES THAT ARE NOT YET ENDED
        private void FilterDataGrid()
        {
            foreach(Tour tour in Items.ToList())
            {
                int[] keyPoints = tourService.GetTourKeypoints(tour.Id);
                if(!keyPointsService.CheckIfKeyPointsPassed(keyPoints))
                {
                    Items.Remove(tour);
                }
            }
        }
        #endregion

        #region COMMANDS
        private void BackCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristHomeView.xaml", UriKind.Relative));
        }

        private void SubmitCommandExecute()
        {
            TourRating tourRating = new TourRating(ratingService.GenerateId(),userService.GetLoginUser().Id,TourGuideKnowledge,TourGuideLanguageProficiency,InterestLevel,Comment,ImageUrl);
            ratingService.Add(tourRating);
            MessageBoxResult result = MessageBox.Show("Your rating was registered successfuly.", "Thank you. We appreciate it!", MessageBoxButton.OK);
            if (result == MessageBoxResult.OK)
            {
                TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristHomeView.xaml", UriKind.Relative));
            }
        }
        public void SetService()
        {
            tourService = new TourService();
            keyPointsService = new KeyPointsService();
            reservationService = new TourReservationService();
            userService = new UserService();
            ratingService = new TourRatingService();
        }

        #endregion

        #region PROPERTIES
        public ObservableCollection<Tour> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        public RelayCommand BackCommand
        {
            get
            {
                return backCommand ?? (backCommand = new RelayCommand(param => BackCommandExecute()));
            }
        }

        public RelayCommand SubmitCommand
        {
            get
            {
                return submitCommand ?? (submitCommand= new RelayCommand(param => SubmitCommandExecute()));
            }
        }

        public int TouristId
        {
            get { return touristId; }
            set
            {
                touristId = value;
                OnPropertyChanged(nameof(TouristId));
            }
        }

        public int TourGuideKnowledge
        {
            get { return tourGuideKnowledge; }
            set
            {
                tourGuideKnowledge = value;
                OnPropertyChanged(nameof(TourGuideKnowledge));
            }
        }

        public int TourGuideLanguageProficiency
        {
            get { return tourGuideLanguageProficiency; }
            set
            {
                tourGuideLanguageProficiency = value;
                OnPropertyChanged(nameof(TourGuideLanguageProficiency));
            }
        }

        public int InterestLevel
        {
            get { return interestLevel; }
            set
            {
                interestLevel = value;
                OnPropertyChanged(nameof(InterestLevel));
            }
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
        }


        #endregion
    }
}
