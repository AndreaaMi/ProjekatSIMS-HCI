using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class ForumCommentService 
    {
        public void Add(ForumComment forum)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.ForumComments.Add(forum);
            unitOfWork.Save();
        }

        public void Edit(ForumComment forum)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.ForumComments.Edit(forum);
            unitOfWork.Save();
        }

        public void Remove(ForumComment forum)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.ForumComments.Remove(forum);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.ForumComments.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.ForumComments.GetAll();
        }

        public IEnumerable<Entity> Search(string term = "")
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.ForumComments.Search(term);
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.ForumComments.GenerateId();
        }
    }
}
