using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekatSIMS.UI.Dialogs.ViewModel
{
    internal class LoginViewModel : ViewModelBase
    {
      /*  private RelayCommand loginCommand;
        private RelayCommand exitCommand;

        private string username;
        private string password;

        private UserService userService = new UserService();

        public LoginViewModel()
        {
            username = string.Empty;
            password = string.Empty;
        }

        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new RelayCommand(param => LoginCommandExecute(), param => CanLoginCommandExecute()));
            }
        }

        public RelayCommand ExitCommand
        {
            get
            {
                return exitCommand ?? (exitCommand = new RelayCommand(param => ExitCommandExecute()));
            }
        }

        private bool CanLoginCommandExecute()
        {
            return Username != null && Password != null;
        }

        private void LoginCommandExecute()
        {
            if (userService.CheckLogin(Username, Password))
            {

                ShowWindow(userService.GetLoginUser());
            }
            else
            {
                Username = string.Empty;
            }
        }

        private void ExitCommandExecute()
        {
            App.Current.Shutdown();
        }


        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public void ShowWindow(User loginUser)
        {
            if (loginUser.UserType == UserType.GUEST1)
            {
                AccommodationSearchView view = new AccommodationSearchView();
                Application.Current.MainWindow = view;
                view.Show();
            }

           
        }*/

    }
}
