using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class VoucherService
    {
        public void Add(Voucher voucher)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Vouchers.Add(voucher);
            unitOfWork.Save();
        }

        public void Edit(Voucher voucher)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Vouchers.Edit(voucher);
            unitOfWork.Save();
        }

        public void Remove(Voucher voucher)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Vouchers.Remove(voucher);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Vouchers.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            IEnumerable<Entity> vouchers = unitOfWork.Vouchers.GetAll();
            return vouchers;
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Vouchers.GenerateId();
        }
    }
}
