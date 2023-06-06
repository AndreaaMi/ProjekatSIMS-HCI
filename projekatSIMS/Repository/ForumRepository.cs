using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class ForumRepository : Repository<Forum>
    {
        public override void Edit(Entity entity)
        {
            Entity forum = base.Get(entity.Id);

            ((Forum)forum).Id = ((Forum)entity).Id;
            ((Forum)forum).GuestName = ((Forum)entity).GuestName;
            ((Forum)forum).Location = ((Forum)entity).Location;
            ((Forum)forum).OpeningComment = ((Forum)entity).OpeningComment;
            ((Forum)forum).IsOpen = ((Forum)entity).IsOpen;
            ((Forum)forum).Comments = ((Forum)entity).Comments;

        }
    }
}
