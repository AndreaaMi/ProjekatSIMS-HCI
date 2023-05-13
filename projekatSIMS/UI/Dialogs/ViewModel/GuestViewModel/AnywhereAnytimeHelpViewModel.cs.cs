using projekatSIMS.CompositeComon;
using projekatSIMS.UI.Dialogs.View.GuestView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace projekatSIMS.UI.Dialogs.ViewModel.GuestViewModel
{
    public class AnywhereAnytimeHelpViewModel : ViewModelBase
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

        public ICommand ShowAnywhereAnytimeCommand { get; private set; }

        public AnywhereAnytimeHelpViewModel()
        {
            ShowAnywhereAnytimeCommand = new RelayCommand(ShowAnywhereAnytimeControl);

        }

        private void ShowAnywhereAnytimeControl(object parameter)
        {
            SelectedView = new AnywhereAnytimeView();
        }
    }
}
