using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
   public class User : Entity
    {
        private string firstName;
        private string lastName;
        private string email;
        private string password;

        private UserType userType;

        public User() { }

        public User(User user, int id)
        {
          
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
        public string  LastName
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

        public UserType UserType
        {
            get { return userType; }
            set 
            {
                userType = value;
                OnPropertyChanged(nameof(UserType));
            }
        }

        public override string ExportToString()
        {
            return id + "|" + firstName + "|" + lastName + "|" + email + "|" + password + "|" + userType.ToString();
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts); //sluzi za id
            FirstName = parts[1];
            LastName = parts[2];
            Email = parts[3];
            Password = parts[4];
            UserType = (UserType)Enum.Parse(typeof(UserType), parts[5]);
        }
    }
}
