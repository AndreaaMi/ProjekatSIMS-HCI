using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public interface IVoucherRepository
    {
        void Add(Entity voucher);

        void Remove(Entity voucher);

        Entity Get(int id);

        IEnumerable<Entity> GetAll();

        int GenerateId();

        void Save();
    }
}
