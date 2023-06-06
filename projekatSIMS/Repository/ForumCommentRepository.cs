using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class ForumCommentRepository : Repository<ForumComment>
    {
        public override void Edit(Entity entity)
        {
            Entity forumComment = base.Get(entity.Id);

            ((ForumComment)forumComment).Id = ((ForumComment)entity).Id;
            ((ForumComment)forumComment).ForumId = ((ForumComment)entity).ForumId;
            ((ForumComment)forumComment).GuestName = ((ForumComment)entity).GuestName;
            ((ForumComment)forumComment).Comment = ((ForumComment)entity).Comment;
            ((ForumComment)forumComment).IsGuestVisited = ((ForumComment)entity).IsGuestVisited;

        }
    }

}
