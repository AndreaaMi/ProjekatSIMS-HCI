using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace projekatSIMS.UI.Dialogs.ViewModel
{
    public class RateNowViewModel : ViewModelBase
    {

        private UserControl _selectedView;

        public UserControl SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                OnPropertyChanged(nameof(SelectedView));
            }
        }
        private string generalRefurbishment;
        private string bathroomRenovation;
        private string furnitureRenovation;
        private string technicalInstallationsUpgrade;
        private string recreationAreaRefurbishment;
        private string safetyImprovements;
        private string comment;
        private AccommodationReservation selectedReservation;
        private string cleanlinessText;
        private string ownerPolitenessText;
        private string commentText;
        private string imageUrlText;
        private UserService userService;
        private ObservableCollection<AccommodationReservation> reservationItems = new ObservableCollection<AccommodationReservation>();
        private AccommodationReservationService accommodationReservationService;
        private AccommodationOwnerRatingService accommodationOwnerRatingService;
        private AccommodationRenovationRecommendationService accommodationRenovationRecommendationService;  

        public AccommodationReservation SelectedReservation
        {
            get { return selectedReservation; }
            set
            {
                if (selectedReservation != value)
                {
                    selectedReservation = value;
                    OnPropertyChanged(nameof(SelectedReservation));
                    OnPropertyChanged(nameof(IsReservationSelected));
                }
            }
        }

        private string errorLabel;
        public string ErrorLabel
        {
            get { return errorLabel; }
            set
            {
                errorLabel = value;
                OnPropertyChanged(nameof(ErrorLabel));
            }
        }

        private string successfulLabel;
        public string SuccessfulLabel
        {
            get { return successfulLabel; }
            set
            {
                successfulLabel = value;
                OnPropertyChanged(nameof(SuccessfulLabel));
            }
        }

        public bool IsReservationSelected => selectedReservation != null;

        public string CleanlinessText
        {
            get { return cleanlinessText; }
            set
            {
                if (cleanlinessText != value)
                {
                    cleanlinessText = value;
                    OnPropertyChanged(nameof(CleanlinessText));
                }
            }
        }
        public string OwnerPolitenessText
        {
            get { return ownerPolitenessText; }
            set
            {
                if (ownerPolitenessText != value)
                {
                    ownerPolitenessText = value;
                    OnPropertyChanged(nameof(OwnerPolitenessText));
                }
            }
        }

        public string CommentText
        {
            get { return commentText; }
            set
            {
                if (commentText != value)
                {
                    commentText = value;
                    OnPropertyChanged(nameof(CommentText));
                }
            }
        }

        public string ImageUrlText
        {
            get { return imageUrlText; }
            set
            {
                if (imageUrlText != value)
                {
                    imageUrlText = value;
                    OnPropertyChanged(nameof(ImageUrlText));
                }
            }
        }
        public string GeneralRefurbishment
        {
            get { return generalRefurbishment; }
            set
            {
                generalRefurbishment = value;
                OnPropertyChanged(nameof(GeneralRefurbishment));
            }
        }

        public string BathroomRenovation
        {
            get { return bathroomRenovation; }
            set
            {
                bathroomRenovation = value;
                OnPropertyChanged(nameof(BathroomRenovation));
            }
        }

        public string FurnitureRenovation
        {
            get { return furnitureRenovation; }
            set
            {
                furnitureRenovation = value;
                OnPropertyChanged(nameof(FurnitureRenovation));
            }
        }

        public string TechnicalInstallationsUpgrade
        {
            get { return technicalInstallationsUpgrade; }
            set
            {
                technicalInstallationsUpgrade = value;
                OnPropertyChanged(nameof(TechnicalInstallationsUpgrade));
            }
        }

        public string RecreationAreaRefurbishment
        {
            get { return recreationAreaRefurbishment; }
            set
            {
                recreationAreaRefurbishment = value;
                OnPropertyChanged(nameof(RecreationAreaRefurbishment));
            }
        }


        public string SafetyImprovements
        {
            get { return safetyImprovements; }
            set
            {
                safetyImprovements = value;
                OnPropertyChanged(nameof(SafetyImprovements));
            }
        }

        public ICommand RateCommand { get; }

        public ICommand RecommendRenovationCommand { get; }

        public RateNowViewModel()
        {
            RecommendRenovationCommand = new RelayCommand(RecommendRenovation, CanRecommendRenovation);
            accommodationRenovationRecommendationService = new AccommodationRenovationRecommendationService();  
            accommodationReservationService = new AccommodationReservationService();
            accommodationOwnerRatingService = new AccommodationOwnerRatingService();
            RateCommand = new RelayCommand(RateReservation, CanRateReservation);
            userService = new UserService();    
            accommodationReservationService = new AccommodationReservationService();
            LoadData();
        }
        private bool CanRateReservation(object parameter)
        {
            return IsReservationSelected;
        }

        private bool CanRecommendRenovation(object parameter)
        {
            return IsReservationSelected;
        }

        private void RecommendRenovation(object parameter)
        {
            if (!ValidateReservationSelection())
                return;

            // Validacija i preuzimanje vrednosti TextBox-ova za preporuku renovacije
            if (!AreRenovationRecommendationInputsValid())
            {
                MessageBox.Show("Please fill in all the renovation recommendation fields.");
                return;
            }

            AccommodationRenovationRecommendation recommendation = new AccommodationRenovationRecommendation
            {
                Id = accommodationRenovationRecommendationService.GenerateId(),
                GeneralRefurbishment = generalRefurbishment,
                BathroomRenovation = bathroomRenovation,
                FurnitureRenovation = furnitureRenovation,
                TechnicalInstallationsUpgrade = technicalInstallationsUpgrade,
                RecreationAreaRefurbishment = recreationAreaRefurbishment,
                SafetyImprovements = safetyImprovements,
                Comment = comment
            };

            accommodationRenovationRecommendationService.Add(recommendation);

            MessageBox.Show("Renovation recommendation submitted successfully.");
        }

        private bool AreRenovationRecommendationInputsValid()
        {
            int minRating = 1;
            int maxRating = 5;

            if (!int.TryParse(GeneralRefurbishment, out int generalRefurbishmentValue)
                || generalRefurbishmentValue < minRating
                || generalRefurbishmentValue > maxRating)
            {
                MessageBox.Show("Please enter a valid rating (1-5) for general refurbishment. Ratings should be whole numbers between 1 and 5.");
                return false;
            }

            if (!int.TryParse(BathroomRenovation, out int bathroomRenovationValue)
                || bathroomRenovationValue < minRating
                || bathroomRenovationValue > maxRating)
            {
                MessageBox.Show("Please enter a valid rating (1-5) for bathroom renovation. Ratings should be whole numbers between 1 and 5.");
                return false;
            }

            if (!int.TryParse(FurnitureRenovation, out int furnitureRenovationValue)
                || furnitureRenovationValue < minRating
                || furnitureRenovationValue > maxRating)
            {
                MessageBox.Show("Please enter a valid rating (1-5) for furniture renovation. Ratings should be whole numbers between 1 and 5.");
                return false;
            }

            if (!int.TryParse(TechnicalInstallationsUpgrade, out int technicalInstallationsUpgradeValue)
                || technicalInstallationsUpgradeValue < minRating
                || technicalInstallationsUpgradeValue > maxRating)
            {
                MessageBox.Show("Please enter a valid rating (1-5) for technical installations upgrade. Ratings should be whole numbers between 1 and 5.");
                return false;
            }

            if (!int.TryParse(RecreationAreaRefurbishment, out int recreationAreaRefurbishmentValue)
                || recreationAreaRefurbishmentValue < minRating
                || recreationAreaRefurbishmentValue > maxRating)
            {
                MessageBox.Show("Please enter a valid rating (1-5) for recreation area refurbishment. Ratings should be whole numbers between 1 and 5.");
                return false;
            }

            if (!int.TryParse(SafetyImprovements, out int safetyImprovementsValue)
                || safetyImprovementsValue < minRating
                || safetyImprovementsValue > maxRating)
            {
                MessageBox.Show("Please enter a valid rating (1-5) for safety improvements. Ratings should be whole numbers between 1 and 5.");
                return false;
            }
            return true;
        }

        private void RateReservation(object parameter)
        {
            if (!ValidateReservationSelection())
                return;

            if (selectedReservation.GuestsRate)
            {
                SuccessfulLabel = "";
                ErrorLabel = "You have already rated this reservation.";
                return;
            }

            if (!IsRatingPeriodValid(selectedReservation))
            {
                SuccessfulLabel = "";
                ErrorLabel = "Rating period has expired.";
                return;
            }

            int cleanliness;
            int ownerPoliteness;

            if (!int.TryParse(CleanlinessText, out cleanliness) || !int.TryParse(OwnerPolitenessText, out ownerPoliteness))
            {
                SuccessfulLabel = "";
                ErrorLabel = "Please enter valid ratings.";
                return;
            }

            string comment = CommentText?.Trim();
            string imageUrl = ImageUrlText?.Trim();

            if (!AreRatingInputsValid(cleanliness, ownerPoliteness, comment))
            {
                SuccessfulLabel = "";
                ErrorLabel = "Please fill in all the rating fields.";
                return;
            }

            AccommodationOwnerRating rating = new AccommodationOwnerRating
            {
                Id = accommodationOwnerRatingService.GenerateId(),
                ReservationId = selectedReservation.Id,
                Cleanliness = cleanliness,
                OwnerPoliteness = ownerPoliteness,
                Comment = comment,
                ImageUrl = imageUrl
            };
            accommodationOwnerRatingService.Add(rating);
            int owner = accommodationOwnerRatingService.GetRatingOwnerId(rating);
            userService.UpdateOwnerRating(owner, ownerPoliteness, cleanliness);
            userService.SetSuperOwner(owner);

            selectedReservation.GuestsRate = true;
            AccommodationReservationService reservationService = new AccommodationReservationService();
            reservationService.Edit(selectedReservation);

            ErrorLabel = "";
            SuccessfulLabel = "Rating submitted successfully.";
        }

        private bool IsRatingPeriodValid(AccommodationReservation reservation)
        {
            DateTime currentDate = DateTime.Now.Date;
            DateTime endDate = reservation.EndDate.Date;
            return (currentDate - endDate).Days <= 5;
        }

        private bool AreRatingInputsValid(int cleanliness, int ownerPoliteness, string comment)
        {
            return cleanliness >= 1 && cleanliness <= 5 &&
                   ownerPoliteness >= 1 && ownerPoliteness <= 5 &&
                   !string.IsNullOrEmpty(comment);
        }

        private bool ValidateReservationSelection()
        {
            if (!IsReservationSelected)
            {
                SuccessfulLabel = "";
                ErrorLabel = "Please select a reservation.";
                return false;
            }
            return true;
        }

        public void LoadData()
        {
            foreach (AccommodationReservation reservation in accommodationReservationService.GetAll().Cast<AccommodationReservation>())
            {
                // Dodaj proveru da se rezervacija nije ocenjena od strane gosta i da je završena
                if (!reservation.GuestsRate && reservation.EndDate < DateTime.Today)
                {
                    ReservationItems.Add(reservation);
                }
            }
        }

        public ObservableCollection<AccommodationReservation> ReservationItems
        {
            get { return reservationItems; }
            set
            {
                reservationItems = value;
                OnPropertyChanged(nameof(reservationItems));
            }
        }

        private string _comment1;

        public string Comment1
        {
            get { return _comment1; }
            set
            {
                if (_comment1 != value)
                {
                    _comment1 = value;
                    OnPropertyChanged(nameof(Comment1));
                }
            }
        }
    }
}
