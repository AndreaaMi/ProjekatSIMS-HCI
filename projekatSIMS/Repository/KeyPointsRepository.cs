using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ((KeyPoints)kp).Belongs = ((KeyPoints)entity).Belongs;
        }
    }
}
