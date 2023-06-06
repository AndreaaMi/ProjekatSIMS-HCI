using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class ComplexTourRequestService
    {
        public void Add(ComplexTourRequest complexTourRequest)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.ComplexTourRequests.Add(complexTourRequest);
            unitOfWork.Save();
        }

        public void Edit(ComplexTourRequest complexTourRequest)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.ComplexTourRequests.Edit(complexTourRequest);
            unitOfWork.Save();
        }

        public void Remove(ComplexTourRequest complexTourRequest)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.ComplexTourRequests.Remove(complexTourRequest);
            unitOfWork.Save();
        }

        public Entity Get(int Id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.ComplexTourRequests.Get(Id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork= new UnitOfWork();
            IEnumerable<Entity> complexTourRequests = unitOfWork.ComplexTourRequests.GetAll();
            return complexTourRequests;
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.ComplexTourRequests.GenerateId();
        }
    }
}
