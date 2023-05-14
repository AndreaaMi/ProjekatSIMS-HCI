using projekatSIMS.Repository;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekatSIMS.Model
{
    public class User : Entity
    {
        private string firstName;
        private string lastName;
        private string email;
        private string password;
        private int age;

        private UserType userType;
        private int reviewCount;
        private double averageRating;
        private bool superStatus;

        private bool isSuperGuest;
        private int reservationCount;
        private int bonusPoints;
        private DateTime superGuestExpirationDate;

        public User() { }

        public User(User user, int id)
        {

        }

        public bool IsSuperGuest
        {
            get { return isSuperGuest; }
            set { isSuperGuest = value; }
        }

        public int ReservationCount
        {
            get { return reservationCount; }
            set { reservationCount = value; }
        }

        public int BonusPoints
        {
            get { return bonusPoints; }
            set { bonusPoints = value; }
        }

        public DateTime SuperGuestExpirationDate
        {
            get { return superGuestExpirationDate; }
            set { superGuestExpirationDate = value; }
        }

       

        public User(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;

        }

        public void BecomeSuperGuest()
        {
            if (reviewCount >= 10)
            {
                superStatus = true;
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
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

        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public UserType UserType
        {
            get { return userType; }
            set
            {
                userType = value;
                OnPropertyChanged(nameof(UserType));
            }
        }

        public int ReviewCount
        {
            get { return reviewCount; }
            set
            {
                reviewCount = value;
                OnPropertyChanged(nameof(ReviewCount));
            }
        }

        public double AverageRating
        {
            get { return averageRating; }
            set
            {
                averageRating = value;
                OnPropertyChanged(nameof(AverageRating));
            }
        }

        public bool SuperStatus
        {
            get { return superStatus; }
            set
            {
                superStatus = value;
                OnPropertyChanged(nameof(SuperStatus));
            }
        }

        public void SetUserType(string part)
        {
            switch (part)
            {
                case "OWNER":
                    UserType = UserType.OWNER;
                    break;

                case "GUEST":
                    UserType = UserType.GUEST;
                    break;

                case "TOURGUIDE":
                    UserType = UserType.TOURGUIDE;
                    break;

                case "TOURIST":
                    UserType = UserType.TOURIST;
                    break;

            }
        }

        public override string ExportToString()
        {
            return id + "|" + firstName + "|" + lastName + "|" + email + "|" + password + "|" + userType.ToString() + "|" + reviewCount + "|" + averageRating + "|" + superStatus + "|" + age + "|" + isSuperGuest + "|" + reservationCount + "|" + bonusPoints +"|"+ superGuestExpirationDate.ToString("yyyy-MM-dd");
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts); //sluzi za id
            FirstName = parts[1];
            LastName = parts[2];
            Email = parts[3];
            Password = parts[4];
            SetUserType(parts[5]);
            ReviewCount = int.Parse(parts[6]);
            AverageRating = double.Parse(parts[7]);
            SuperStatus = bool.Parse(parts[8]);
            Age = int.Parse(parts[9]);
            IsSuperGuest = bool.Parse(parts[10]);
            ReservationCount = int.Parse(parts[11]);
            BonusPoints = int.Parse(parts[12]);
            SuperGuestExpirationDate = DateTime.ParseExact(parts[13], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}