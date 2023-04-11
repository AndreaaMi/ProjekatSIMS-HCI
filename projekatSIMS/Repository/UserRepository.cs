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
            ((User)user).AverageRating = ((User)entity).AverageRating;
            ((User)user).ReviewCount = ((User)entity).ReviewCount;
            ((User)user).SuperStatus = ((User)entity).SuperStatus;
            ((User)user).Age = ((User)entity).Age;
        }

        public void SetLoginUser(User user)
        {
            DataContext.Instance.LoginUser = user;
        }

        public User GetLoginUser()
        {
            return DataContext.Instance.LoginUser;
        }


        public string GetLoginUserType()
        {
            return DataContext.Instance.LoginUser.UserType.ToString();
        }

       
    }
}
