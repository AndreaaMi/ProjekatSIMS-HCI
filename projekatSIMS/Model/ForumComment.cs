using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projekatSIMS.Model
{
    public class ForumComment : Entity
    {
        private int forumId;
        private string guestName;
        private string comment;
        private bool isGuestVisited;

        public ForumComment()
        {
        }
        public ForumComment(int forumId, string guestName, string comment, bool isGuestVisited)
        {
            this.forumId = forumId;
            this.guestName = guestName;
            this.comment = comment;
            this.isGuestVisited = isGuestVisited;
        }

        public int ForumId
        {
            get { return forumId; }
            set
            {
                forumId = value;
                OnPropertyChanged(nameof(ForumId));
            }
        }
        public string GuestName
        {
            get { return guestName; }
            set
            {
                guestName = value;
                OnPropertyChanged(nameof(GuestName));
            }
        }
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public bool IsGuestVisited
        {
            get { return isGuestVisited; }
            set
            {
                isGuestVisited = value;
                OnPropertyChanged(nameof(IsGuestVisited));
            }
        }

        public override string ExportToString()
        {
            return id + "|" + forumId + "|" + guestName + "|" + comment + "|" + isGuestVisited.ToString();
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            forumId = int.Parse(parts[1]);
            guestName = parts[2];
            comment = parts[3];
            isGuestVisited = bool.Parse(parts[4]);
        }
    }
}
