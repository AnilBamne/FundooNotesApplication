using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBusiness
    {
        public CollabEntity AddCollaborator(string collabEmail, long noteId, long userId);
        public List<CollabEntity> GetCollabs(long noteId);
        public bool DeleteCollab(long noteId, long userId);
    }
}
