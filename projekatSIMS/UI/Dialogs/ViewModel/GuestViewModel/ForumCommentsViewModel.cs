using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace projekatSIMS.UI.Dialogs.ViewModel.GuestViewModel
{
    public class ForumCommentsViewModel:ViewModelBase
    {
        ForumService forumService = new ForumService();
        ForumCommentService forumCommentService = new ForumCommentService();
        UserService userService = new UserService();
        public ICommand AddCommentCommand { get; private set; }

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

        public ForumCommentsViewModel()
        {
            MoveToPreviousCommentCommand = new RelayCommand(MoveToPreviousComment);
            MoveToNextCommentCommand = new RelayCommand(MoveToNextComment);
            AddCommentCommand = new RelayCommand(AddComment);
            LoadData();
        }

        private ObservableCollection<ForumComment> forumComments = new ObservableCollection<ForumComment>();
        private Forum selectedOpeningComment;
        private int currentCommentIndex;

        private void AddComment(object parameter)
        {
            User currentUser = userService.GetLoginUser();
            if (selectedOpeningComment == null)
            {
                CancelMessage = "You must select an opening comment.";
                SuccessMessage = "";
                return;
            }

            // Provera da li je komentar unesen
            if (string.IsNullOrEmpty(Comment))
            {
                CancelMessage= "You must enter a comment.";
                SuccessMessage = "";
                return;
            }
            var newForum = new ForumComment
            {
                Id = forumCommentService.GenerateId(),
                ForumId = selectedOpeningComment.Id,
                GuestName = currentUser.FirstName,
                Comment = Comment,
                IsGuestVisited = true,
            };
            forumCommentService.Add(newForum);

            LoadCommentsForOpening();

            // Pronađite indeks novododanog komentara
            int newIndex = ForumComments.IndexOf(newForum);
            if (newIndex != -1)
            {
                currentCommentIndex = newIndex;
                OnPropertyChanged(nameof(CurrentComment));
            }
            SuccessMessage = "Comment successfully added.";
            CancelMessage = "";
            Comment = "";


        }
        public ObservableCollection<ForumComment> ForumComments
        {
            get { return forumComments; }
            set
            {
                forumComments = value;
                OnPropertyChanged(nameof(ForumComments));
            }
        }

        public Forum SelectedOpeningComment
        {
            get { return selectedOpeningComment; }
            set
            {
                selectedOpeningComment = value;
                OnPropertyChanged(nameof(SelectedOpeningComment));
                LoadCommentsForOpening();
            }
        }

        public ICommand MoveToPreviousCommentCommand { get; }
        public ICommand MoveToNextCommentCommand { get; }


        private void LoadCommentsForOpening()
        {
            ForumComments.Clear();
            currentCommentIndex = -1;

            if (selectedOpeningComment != null)
            {
                foreach (ForumComment forumComment in forumCommentService.GetAll().Cast<ForumComment>())
                {
                    if (selectedOpeningComment != null && selectedOpeningComment.Id == forumComment.ForumId)
                    {
                        ForumComments.Add(forumComment);
                    }
                }

                if (ForumComments.Count > 0)
                {
                    currentCommentIndex = 0;
                    OnPropertyChanged(nameof(CurrentComment));
                }
            }

            UpdateCommentGuestVisited();
        }

        private void UpdateCommentGuestVisited()
        {
            if (ForumComments.Count > 0)
            {
                IsCommentGuestVisited = ForumComments[currentCommentIndex].IsGuestVisited;
            }
            else
            {
                IsCommentGuestVisited = false;
            }
        }
        private bool isCommentGuestVisited;

        public bool IsCommentGuestVisited
        {
            get { return isCommentGuestVisited; }
            set
            {
                isCommentGuestVisited = value;
                OnPropertyChanged(nameof(IsCommentGuestVisited));
            }
        }

        private void MoveToPreviousComment(object parameter)
        {
            if (currentCommentIndex > 0)
            {
                currentCommentIndex--;
                OnPropertyChanged(nameof(CurrentComment));
            }
            UpdateCommentGuestVisited();
        }

        private void MoveToNextComment(object parameter)
        {
            if (currentCommentIndex < ForumComments.Count - 1)
            {
                currentCommentIndex++;
                OnPropertyChanged(nameof(CurrentComment));
            }
            UpdateCommentGuestVisited();
        }


        public string CurrentComment
        {
            get
            {
                if (currentCommentIndex >= 0 && currentCommentIndex < ForumComments.Count)
                {
                    return ForumComments[currentCommentIndex].Comment;
                }
                return null;
            }
        }

        private void LoadData()
        {

            foreach (Forum forum in forumService.GetAll().Cast<Forum>())
            {
                 ForumItems.Add(forum);
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

        private bool isGuestVisited;

        public bool IsGuestVisited
        {
            get { return isGuestVisited; }
            set
            {
                if (isGuestVisited != value)
                {
                    isGuestVisited = value;
                    OnPropertyChanged(nameof(IsGuestVisited));
                }
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
        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        private int forumId;
        public int ForumId
        {
            get { return forumId; }
            set
            {
                forumId = value;
                OnPropertyChanged(nameof(ForumId));
            }
        }
    }
}
