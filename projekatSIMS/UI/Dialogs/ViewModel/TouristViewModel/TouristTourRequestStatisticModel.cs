using LiveCharts;
using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristTourRequestStatisticModel : ViewModelBase
    {
        private ObservableCollection<TourRequest> items = new ObservableCollection<TourRequest>();
        private SeriesCollection seriesCollection;
        private string[] labels;
        private Func<string, string> values;

        private TourRequestService tourRequestService;
        private UserService userService;
        public TouristTourRequestStatisticModel()
        {
            SetService();
            LoadData();
        }
        public void SetService()
        {
            tourRequestService = new TourRequestService();
            userService = new UserService();
        }

        public void LoadData()
        {
            foreach(TourRequest item in tourRequestService.GetAll())
            {
                if(item.GuestId == userService.GetLoginUser().Id)
                {
                    Items.Add(item);
                }
            }
        }
        public ObservableCollection<TourRequest> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public SeriesCollection SeriesCollection
        {
            get { return seriesCollection; }
            set
            {
                seriesCollection = value;
                OnPropertyChanged(nameof(SeriesCollection));
            }
        }

        public string[] Labels
        {
            get { return labels; }
            set
            {
                labels = value;
                OnPropertyChanged(nameof(Labels));
            }
        }

        public Func<string,string> Values
        {
            get { return values; }
            set
            {
                values = value;
                OnPropertyChanged(nameof(Values));
            }
        }
    }
}
