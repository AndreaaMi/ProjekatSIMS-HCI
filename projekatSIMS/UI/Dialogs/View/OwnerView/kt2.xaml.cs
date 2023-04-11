using projekatSIMS.Model;
using projekatSIMS.Service;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace projekatSIMS.UI.Dialogs.View.OwnerView
{
    /// <summary>
    /// Interaction logic for kt2.xaml
    /// </summary>
    public partial class kt2 : Window

    {
        ReservationRescheduleRequestService reservationRescheduleRequestService = new ReservationRescheduleRequestService();
        UserService userService = new UserService();
        AccommodationService accommodationService = new AccommodationService();




        public kt2()
        {
            InitializeComponent();

        }


        private void ReschedulePosibillityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            User owner = userService.GetLoginUser();
            if (IdRequestTextBox.Text == "")
            {
                MessageBox.Show("Unesite Id zahteva koji zelite da obradite!");
                return;
            }
            ReservationRescheduleRequest currentRequest = (ReservationRescheduleRequest)reservationRescheduleRequestService.Get(int.Parse(IdRequestTextBox.Text));
            if (currentRequest != null)
            {
                reservationRescheduleRequestService.AcceptRequest(currentRequest);
                MessageBox.Show("Zahtev prihvacen!");

                RequestsListBox.Items.Clear();
                foreach (ReservationRescheduleRequest entity in reservationRescheduleRequestService.GetAll())
                {
                    if (entity.Status == RequestStatusType.PENDING && (reservationRescheduleRequestService.GetRequestOwnerId(entity) == owner.Id))
                    {
                        RequestsListBox.Items.Add(entity.ReservationId + "-" + entity.NewStartDate + "-" + entity.NewEndDate + "|" + entity.GuestName);

                    }

                }
            }
            else
            {
                MessageBox.Show("Izaberite zahtev koji zelite da obradite.");

            }



        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            CommentTextBox.Visibility = Visibility.Visible;
            FinishButton.Visibility = Visibility.Visible;

        }

        private void ReviewsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ShowReviewsButton_Click(object sender, RoutedEventArgs e)
        {

            User owner = userService.GetLoginUser();
            ReviewsListBox.Items.Clear();
            AccommodationOwnerRatingService accommodationOwnerRatingService = new AccommodationOwnerRatingService();


            foreach (AccommodationOwnerRating entity in accommodationOwnerRatingService.GetAll())
            {
                if (accommodationOwnerRatingService.GetRatingOwnerId(entity) == owner.Id)
                {
                    ReviewsListBox.Items.Add(entity.Id + "|" + entity.Cleanliness + "|" + entity.OwnerPoliteness + "|komentar :" + entity.Comment);
                }
            }




        }

        private void ShowRequestsButton_Click(object sender, RoutedEventArgs e)
        {

            RequestsListBox.Items.Clear();
            ReservationRescheduleRequestService reservationRescheduleRequestService = new ReservationRescheduleRequestService();
            User owner = userService.GetLoginUser();
            List<Entity> accommodations = new List<Entity>();

            foreach (ReservationRescheduleRequest entity in reservationRescheduleRequestService.GetAll())
            {
                if (reservationRescheduleRequestService.GetRequestOwnerId(entity) == owner.Id)
                {
                    RequestsListBox.Items.Add(entity.ReservationId + "-" + entity.NewStartDate + "-" + entity.NewEndDate + "|" + entity.GuestName);

                }


            }

        }

        private void RequestListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {




        }

        private void IdRequestTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (IdRequestTextBox.Text == "")
            {
                MessageBox.Show("Unesite Id zahteva koji zelite da obradite!");
                return;
            }
            ReservationRescheduleRequest currentRequest = (ReservationRescheduleRequest)reservationRescheduleRequestService.Get(int.Parse(IdRequestTextBox.Text));
            if (reservationRescheduleRequestService.AreNewRequestDatesAvailable(currentRequest))
            {
                ReschedulePosibillityTextBox.Text = "NOVI DATUMI DOSTUPNI";
            }
            else
            {
                ReschedulePosibillityTextBox.Text = "DATUMI NISU DOSTUPNI";
            }


        }

        private void CommentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            User owner = userService.GetLoginUser();
            if (IdRequestTextBox.Text == "")
            {
                MessageBox.Show("Unesite Id zahteva koji zelite da obradite!");
                return;
            }
            ReservationRescheduleRequest currentRequest = (ReservationRescheduleRequest)reservationRescheduleRequestService.Get(int.Parse(IdRequestTextBox.Text));
            if (currentRequest != null)
            {
                reservationRescheduleRequestService.RejectRequest(currentRequest, CommentTextBox.Text);
                MessageBox.Show("Zahtev odbijen!");
                FinishButton.Visibility = Visibility.Collapsed;
                CommentTextBox.Visibility = Visibility.Collapsed;

                RequestsListBox.Items.Clear();
                foreach (ReservationRescheduleRequest entity in reservationRescheduleRequestService.GetAll())
                {
                    if (entity.Status == RequestStatusType.PENDING && (reservationRescheduleRequestService.GetRequestOwnerId(entity) == owner.Id))
                    {
                        RequestsListBox.Items.Add(entity.ReservationId + "-" + entity.NewStartDate + "-" + entity.NewEndDate + "|" + entity.GuestName);

                    }

                }
            }
            else
            {
                MessageBox.Show("Izaberite zahtev koji zelite da obradite.");

            }

        }

        private void SuperOwnerButton_Click(object sender, RoutedEventArgs e)
        {
            SuperOwnerView win2 = new SuperOwnerView();
            win2.Show();


        }
    }
}
