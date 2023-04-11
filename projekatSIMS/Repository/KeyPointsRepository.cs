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

    }
}
