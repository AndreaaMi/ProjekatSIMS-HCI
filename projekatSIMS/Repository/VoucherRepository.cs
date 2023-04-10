using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class VoucherRepository : Repository<Voucher>
    {
        public override void Edit(Entity entity)
        {
            Entity voucher = base.Get(entity.Id);

            ((Voucher)voucher).GuestId = ((Voucher)entity).GuestId;
            ((Voucher)voucher).ExpirationDate = ((Voucher)entity).ExpirationDate;
        }
    }
}
