using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class UserRepository : Repository<User>
    {
        //override za edit
        public override void Edit(Entity entity) //prosledjujem izmenjen entitet ali mu je id isti
        {
            Entity user = base.Get(entity.Id);

            ((User)user).Id = ((User)entity).Id;            //I sada menjam vrednosti sa tim da se id ne moze promeniti!
            ((User)user).Email = ((User)entity).Email;
            ((User)user).FirstName = ((User)entity).FirstName;
            ((User)user).LastName = ((User)entity).LastName;
            ((User)user).Password = ((User)entity).Password;
        }

        public void SetLoginUser(User user)
        {
            SIMSContext.Instance.LoginUser = user;
        }

        public User GetLoginUser()
        {
            return SIMSContext.Instance.LoginUser;
        }
    }
}
