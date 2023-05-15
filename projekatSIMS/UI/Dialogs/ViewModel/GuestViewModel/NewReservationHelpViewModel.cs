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
    public class NewReservationHelpViewModel : ViewModelBase
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

        public ICommand ShowNewReservationCommand { get; private set; }

        public NewReservationHelpViewModel()
        {
            ShowNewReservationCommand = new RelayCommand(ShowNewReservationControl);

        }

        private void ShowNewReservationControl(object parameter)
        {
            SelectedView = new NewReservationView();
        }
    }
}
