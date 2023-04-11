using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projekatSIMS.Model
{
    public class Voucher : Entity
    {
        private int guestId;
        private DateTime expirationDate;

        public Voucher()
        {

        }

        public int GuestId
        {
            get { return guestId; }
            set { 
                guestId = value;
                OnPropertyChanged(nameof(GuestId));
            }
        }
    
        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set
            {
                expirationDate = value;
                OnPropertyChanged(nameof(ExpirationDate));
            }
        }

        public override string ExportToString()
        {
            return id + "|" + guestId + "|" + expirationDate.ToString("yyyy-MM-dd");
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            GuestId = int.Parse(parts[1]);
            ExpirationDate = DateTime.ParseExact(parts[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}
