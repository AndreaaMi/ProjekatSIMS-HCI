﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projekatSIMS.Model
{
    public class ReservationRescheduleRequest:Entity
    {
        private int reservationId;
        private DateTime newStartDate;
        private DateTime newEndDate;
        private string guestName;
        private RequestStatusType status;

        public ReservationRescheduleRequest()
        {
        }

        public ReservationRescheduleRequest(int reservationId, DateTime newStartDate, DateTime newEndDate, string guestName, RequestStatusType status)
        {
            this.reservationId = reservationId;
            this.newStartDate = newStartDate;
            this.newEndDate = newEndDate;
            this.guestName = guestName;
            this.status = status;
        }

        public int ReservationId 
        {
            get {  return reservationId; }
            set
            { 
                reservationId = value; 
                OnPropertyChanged(nameof(ReservationId));
            }
        }

        public DateTime NewStartDate
        {
            get { return newStartDate; }
            set
            {
                newStartDate = value;
                OnPropertyChanged(nameof(NewStartDate));
            }
        }
        public DateTime NewEndDate
        {
            get { return newEndDate; }
            set
            {
                newEndDate = value;
                OnPropertyChanged(nameof(NewEndDate));
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

        public RequestStatusType Status
        {
            get { return status; }  
            set
            {
                status = value; 
                OnPropertyChanged(nameof(Status));
            }
        }

        public void SetRequestStatusType(string part)
        {
            switch (part)
            {
                case "PENDING":
                    Status = RequestStatusType.PENDING;
                    break;

                case "REJECTED":
                    Status = RequestStatusType.REJECTED;
                    break;

                case "APPROVED":
                    Status = RequestStatusType.APPROVED;
                    break;
            }
        }
        

        public override string ExportToString()
        {
            return id + "|" + reservationId + "|" + newStartDate.ToString("yyyy-MM-dd") + "|" + newEndDate.ToString("yyyy-MM-dd") + "|" + guestName + "|" + status.ToString();
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);

            reservationId = int.Parse(parts[1]);
            newStartDate = DateTime.ParseExact(parts[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            newEndDate = DateTime.ParseExact(parts[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            guestName = parts[4];
            SetRequestStatusType(parts[5]);

        }
    }
}
