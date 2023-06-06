using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    public class ComplexTourRequest : Entity
    {
        private TourRequestStatus status;
        private List<TourRequest> requests;
        public ComplexTourRequest()
        {
            requests = new List<TourRequest>();
        }

        public TourRequestStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public List<TourRequest> Requests
        {
            get { return requests; }
            set
            {
                requests = value; 
                OnPropertyChanged(nameof(Requests));
            }
        }

        public override string ExportToString()
        {
            return id + "|" + ExportRequests(requests) + "|" + status;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            string[] requestIds = parts[1].Split('_');
            ImportRequests(requestIds);
            SetStatus(parts[2]);
        }

        public string ExportRequests(List<TourRequest> requests)
        {
            if(requests.Count == 0)
            {
                return "NULL";
            }

            string RequestIds = (requests.First()).Id.ToString();

            foreach(TourRequest request in requests)
            {
                if (!(request.Id.ToString().Equals(RequestIds)))
                {
                    RequestIds = RequestIds + "_" + request.Id.ToString();
                }
            }

            return RequestIds;
        }

        public void ImportRequests(string[] requestIds)
        {
            foreach(string requestId in requestIds)
            {
                TourRequest request = new TourRequest()
                {
                    Id = int.Parse(requestId)
                };
                Requests.Add(request);
            }
        }
        public void SetStatus(string part)
        {
            switch (part)
            {
                case "ACCEPTED":
                    Status = TourRequestStatus.ACCEPTED;
                    break;

                case "PENDING":
                    Status = TourRequestStatus.PENDING;
                    break;

                case "REJECTED":
                    Status = TourRequestStatus.REJECTED;
                    break;
            }
        }
    }
}
