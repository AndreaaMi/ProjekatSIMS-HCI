using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace projekatSIMS.UI.Dialogs.ViewModel.GuestViewModel
{
    public class ForumViewModel : ViewModelBase
    {
        AccommodationService accommodationService = new AccommodationService();
        ForumService forumService = new ForumService();
        UserService userService = new UserService();
        public ICommand CloseForumCommand { get; private set; } 
        public ICommand ShowForumCommentsCommand { get; private set; }
        public ICommand OpenForumCommand { get; private set; }  
        public Forum SelectedForum { get; set; }

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

        public ForumViewModel()
        {
            OpenForumCommand = new RelayCommand(OpenForumControl);
            CloseForumCommand = new RelayCommand(CloseForum);
            ShowForumCommentsCommand = new RelayCommand(ShowForumCommnetsControl);
            LoadData();
        }

        private void LoadData()
        {
            foreach (Accommodation accommodation in accommodationService.GetAll().Cast<Accommodation>())
            {
                string location = $"{accommodation.Location.Country} - {accommodation.Location.City}";

                if (!AccommodationLocations.Contains(location))
                {
                    AccommodationLocations.Add(location);
                }
            }

            foreach (Forum forum in forumService.GetAll().Cast<Forum>())
            {
                User currentUser = userService.GetLoginUser();
                if (forum.GuestName == currentUser.FirstName)
                {
                    ForumItems.Add(forum);
                } 

            }
        }

        private ObservableCollection<string> accommodationLocations = new ObservableCollection<string>();

        public ObservableCollection<string> AccommodationLocations
        {
            get { return accommodationLocations; }
            set
            {
                accommodationLocations = value;
                OnPropertyChanged(nameof(AccommodationLocations));
            }
        }
        private ObservableCollection<Forum> forumItems = new ObservableCollection<Forum>();

        public ObservableCollection<Forum> ForumItems
        {
            get { return forumItems; }

            set
            {
                forumItems = value;
                OnPropertyChanged(nameof(forumItems));
            }
        }

        private string _successMessage;
        public string SuccessMessage
        {
            get { return _successMessage; }
            set
            {
                _successMessage = value;
                OnPropertyChanged(nameof(SuccessMessage));
            }
        }

        private string _cancelMessage;
        public string CancelMessage
        {
            get { return _cancelMessage; }
            set
            {
                _cancelMessage = value;
                OnPropertyChanged(nameof(CancelMessage));
            }

        }

        private string _successMessage1;
        public string SuccessMessage1
        {
            get { return _successMessage1; }
            set
            {
                _successMessage1 = value;
                OnPropertyChanged(nameof(SuccessMessage1));
            }
        }

        private string _cancelMessage1;
        public string CancelMessage1
        {
            get { return _cancelMessage1; }
            set
            {
                _cancelMessage1 = value;
                OnPropertyChanged(nameof(CancelMessage1));
            }

        }
        private string guestName;
        public string GuestName
        {
            get { return guestName; }
            set
            {
                guestName = value;
                OnPropertyChanged(nameof(GuestName));
            }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private string openingComment;
        public string OpeningComment
        {
            get { return openingComment; }
            set
            {
                openingComment = value;
                OnPropertyChanged(nameof(OpeningComment));
            }
        }


        private void OpenForumControl(object parameter)
        {
            User currentUser = userService.GetLoginUser();

            if (string.IsNullOrEmpty(Location))
            {
                CancelMessage1 = "Please select a location.";
                SuccessMessage1 = "";
                return;
            }

            if (string.IsNullOrEmpty(OpeningComment))
            {
                CancelMessage1 = "Please enter an opening comment.";
                SuccessMessage1 = "";
                return;
            }

            var newForum = new Forum
            {
                Id = forumService.GenerateId(),
                GuestName = currentUser.FirstName,
                Location = Location,
                OpeningComment = OpeningComment,
                IsOpen = true,
                Comments = new List<string>()
            };

            forumService.Add(newForum);

            Location = null;
            OpeningComment = "";
            SuccessMessage1 = "Forum successfully opened.";
            CancelMessage1 = "";
            ForumItems.Clear();
            LoadData(); 

        }

        private void CloseForum(object parameter)
        {
            if (SelectedForum != null)
            {
                if (SelectedForum.IsOpen)
                {
                    SelectedForum.IsOpen = false;
                    SuccessMessage = "You have successfully closed the forum.";
                    CancelMessage = "";
                    forumService.UpdateForum(SelectedForum);
                }
                else
                {
                    SuccessMessage = "";
                    CancelMessage = "The forum you have selected is already closed.";
                }
            }
            else 
            {
                SuccessMessage = "";
                CancelMessage = "Please select the forum first.";
            }
        }
        private void ShowForumCommnetsControl(object parameter)
        {
            SelectedView = new ForumCommentsView();
        }

    }
}
