using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class TourRequestService
    {
        public void Add(TourRequest tourRequest)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.TourRequests.Add(tourRequest);
            unitOfWork.Save();
        }

        public void Edit(TourRequest tourRequest)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.TourRequests.Edit(tourRequest);
            unitOfWork.Save();
        }

        public void Remove(TourRequest tourRequest)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.TourRequests.Remove(tourRequest);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.TourRequests.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            IEnumerable<Entity> tourRequests = unitOfWork.TourRequests.GetAll();
            return tourRequests;
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.TourRequests.GenerateId();
        }
    }
}
