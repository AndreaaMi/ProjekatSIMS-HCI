using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View;
using projekatSIMS.UI.Dialogs.View.OwnerView;
using projekatSIMS.UI.Dialogs.View.TourGuideView;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace projekatSIMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserService userService = new UserService();
        public MainWindow()
        {
            InitializeComponent();           
        }
        private void DisplayWindowForUserType(string type)
        {
            Window windowToDisplay;
            switch (type)
            {
                case "OWNER":
                    windowToDisplay = new kt2();
                    break;
                case "GUEST":
                    windowToDisplay = new AccommodationSearchView();
                    break;
                case "TOURGUIDE":
                    windowToDisplay = new TourGuideMainWindow(); //1 promeniti na vodicMainWindow
                    break;
                case "TOURIST":
                    windowToDisplay = new TouristMainWindow();
                    break;
                default:
                    throw new ArgumentException("Invalid log in credentials");
            }

            windowToDisplay.Show();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtUser.Text;
            string password = txtPass.Password;
            userService.CheckCredentials(email, password);
            DisplayWindowForUserType(userService.GetLoginUserType());
        }


    }
}
