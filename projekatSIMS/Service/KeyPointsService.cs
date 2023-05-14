using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class KeyPointsService
    {
        public void Add(KeyPoints kp)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.KeyPoint.Add(kp);
            unitOfWork.Save();
        }

        public void Edit(KeyPoints kp)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.KeyPoint.Edit(kp);
            unitOfWork.Save();
        }

        public void Remove(KeyPoints kp)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.KeyPoint.Remove(kp);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.KeyPoint.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            IEnumerable<Entity> kp = unitOfWork.KeyPoint.GetAll();
            return kp;
        }

        public bool CheckIfKeyPointsPassed(int[] keyPointIds)
        {
            UnitOfWork unitOfWork= new UnitOfWork();
            return unitOfWork.KeyPoint.CheckIfKeyPointsPassed(keyPointIds);
        }

        public int GetActiveKeyPointId(int tourId)
        {
            UnitOfWork unitOfWork= new UnitOfWork();
            return unitOfWork.KeyPoint.GetActiveKeyPointId(tourId);
        }

        public bool HasTheTourStarted(int tourId)
        {
            UnitOfWork unitOfWork= new UnitOfWork();
            return unitOfWork.KeyPoint.HasTheTourStarted(tourId);
        }

        public bool HasTheTourEnded(int tourId)
        {
            UnitOfWork unitOfWork= new UnitOfWork();
            return unitOfWork.KeyPoint.HasTheTourEnded(tourId);
        }

    }
}
