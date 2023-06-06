using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class ForumService
    {
        public void Add(Forum forum)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Forums.Add(forum);
            unitOfWork.Save();
        }

        public void Edit(Forum forum)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Forums.Edit(forum);
            unitOfWork.Save();
        }

        public void Remove(Forum forum)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Forums.Remove(forum);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Forums.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Forums.GetAll();
        }

        public IEnumerable<Entity> Search(string term = "")
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Forums.Search(term);
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Forums.GenerateId();
        }
        public void UpdateForum(Forum forum)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Forum existingForum = (Forum)unitOfWork.Forums.Get(forum.Id);
            if (existingForum != null)
            {
                existingForum.IsOpen = forum.IsOpen;
                unitOfWork.Forums.Edit(existingForum);
                unitOfWork.Save();
            }
        }
    }
}
