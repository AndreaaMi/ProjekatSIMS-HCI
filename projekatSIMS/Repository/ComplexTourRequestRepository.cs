using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class ComplexTourRequestRepository : Repository<ComplexTourRequest>
    {
        public override void Edit(Entity entity)
        {
            Entity complexTourRequest = base.Get(entity.Id);

            ((ComplexTourRequest)complexTourRequest).Requests = ((ComplexTourRequest)entity).Requests;
            ((ComplexTourRequest)complexTourRequest).Status = ((ComplexTourRequest)entity).Status;
        }
    }
}
