using projekatSIMS.UI.Dialogs.ViewModel.OwnerViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace projekatSIMS.UI.Dialogs.View.OwnerView
{
    /// <summary>
    /// Interaction logic for OwnerMainWindow.xaml
    /// </summary>
    public partial class OwnerMainWindow : Window
    {
        private OwnerMainWindowModel ownerMainWindowModel;
        public static NavigationService navigationService;
        private static volatile OwnerMainWindow instance;
        public OwnerMainWindow()
        {
            InitializeComponent();
            navigationService = this.frame.NavigationService;
            ownerMainWindowModel = new OwnerMainWindowModel(navigationService);
            DataContext = ownerMainWindowModel;
        }

        public static OwnerMainWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    return instance;
                }
                instance = new OwnerMainWindow();
                return instance;
            }
        }


    }
}
