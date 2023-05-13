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
    public class AllAccommodationHelpViewModel : ViewModelBase
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

        public ICommand ShowAllAccommodationsCommand { get; private set; }

        public AllAccommodationHelpViewModel()
        {
            ShowAllAccommodationsCommand = new RelayCommand(ShowAllAccommodationsControl);

        }

        private void ShowAllAccommodationsControl(object parameter)
        {
            SelectedView = new AllAccommodationsView();
        }
    }
}
