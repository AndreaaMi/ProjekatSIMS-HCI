using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class KeyPointsRepository : Repository<KeyPoints>
    {
        public override void Edit(Entity entity)
        {
            Entity kp = base.Get(entity.Id);

            ((KeyPoints)kp).Id = ((KeyPoints)entity).Id;
            ((KeyPoints)kp).Name = ((KeyPoints)entity).Name;
            ((KeyPoints)kp).IsActive = ((KeyPoints)entity).IsActive;
            ((KeyPoints)kp).AssociatedTour = ((KeyPoints)entity).AssociatedTour;
        }

        public bool CheckIfKeyPointsPassed(int[] keyPointIds)
        {
            foreach (int id in keyPointIds)
            {
                KeyPoints kp = (KeyPoints)DataContext.Instance.Keypoints.FirstOrDefault(k => k.Id == id);
                if (kp == null || !kp.IsActive)
                {
                    return false;
                }
            }
            return true;
        }

        public int GetActiveKeyPointId(int tourId)
        {
            int activeKeyPointId = -1;
            foreach (KeyPoints kp in DataContext.Instance.Keypoints)
            {
                if(kp.AssociatedTour == tourId && kp.IsActive)
                {
                    if (kp.Id > activeKeyPointId)
                    {
                        activeKeyPointId = kp.Id;
                    }
                }
            }
            return activeKeyPointId;
        }

        public bool HasTheTourStarted(int tourId)
        {
            var firstKeyPoint = DataContext.Instance.Keypoints.OfType<KeyPoints>().FirstOrDefault(kp => kp.AssociatedTour == tourId);

            if (firstKeyPoint != null)
            {
                return firstKeyPoint.IsActive;
            }

            return false;
        }

        public bool HasTheTourEnded(int tourId)
        {
            var keyPoints = DataContext.Instance.Keypoints.OfType<KeyPoints>()
                              .Where(kp => kp.AssociatedTour == tourId)
                              .OrderByDescending(kp => kp.Id);

            var lastKeyPoint = keyPoints.FirstOrDefault();

            if (lastKeyPoint != null)
            {
                return lastKeyPoint.IsActive;
            }

            return false;
        }

    }
}
