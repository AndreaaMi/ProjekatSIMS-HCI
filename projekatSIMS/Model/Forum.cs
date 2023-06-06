using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projekatSIMS.Model
{
    public class Forum : Entity
    {
        private string guestName;
        private string location;
        private string openingComment;
        private bool isOpen;
        private List<string> comments;

        public Forum()
        {
        }

        public Forum(string guestName, string location, string openingComment, List<string> comments)
        {
            this.guestName = guestName;
            this.location = location;
            this.openingComment = openingComment;
            this.isOpen = true;
            this.comments = comments;
        }

        public string GuestName
        {
            get { return guestName; }
            set {
                guestName = value;
                OnPropertyChanged(nameof(GuestName));
            }
        }
        public string Location
        {
            get { return location; }
            set { location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public string OpeningComment
        {
            get { return openingComment; }
            set { openingComment = value;
                OnPropertyChanged(nameof(OpeningComment));
            }
        }

        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value;
                OnPropertyChanged(nameof(IsOpen));
            }
        }

        public List<string> Comments
        {
            get { return comments; }
            set { comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        public override string ExportToString()
        {
            string commentsString = string.Join(";", Comments);
            return id + "|" + guestName + "|" + location + "|" + openingComment + "|" + isOpen.ToString() + "|" + commentsString;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            location = parts[2];
            guestName = parts[1];
            openingComment = parts[3];
            isOpen = bool.Parse(parts[4]);
            string[] commentsArray = parts[5].Split(';');
            Comments = new List<string>(commentsArray);


        }
    }
}
