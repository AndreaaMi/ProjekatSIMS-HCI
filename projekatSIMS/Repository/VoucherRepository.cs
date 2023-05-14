using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        public void Add(Entity voucher)
        {
            DataContext.Instance.GetAllEntitiesOfType(typeof(Voucher)).Add(voucher);
        }
        public int GenerateId()
        {
            return DataContext.Instance.GenerateId(DataContext.Instance.GetAllEntitiesOfType(typeof(Voucher)));
        }

        public Entity Get(int id)
        {
            return DataContext.Instance.GetAllEntitiesOfType(typeof(Voucher)).Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Entity> GetAll()
        {
            return DataContext.Instance.GetAllEntitiesOfType(typeof(Voucher));
        }

        public void Remove(Entity voucher)
        {
            DataContext.Instance.GetAllEntitiesOfType(typeof(Voucher)).Remove(voucher);
        }

        public void Save()
        {
            DataContext.Instance.Save();
        }
    }
}
