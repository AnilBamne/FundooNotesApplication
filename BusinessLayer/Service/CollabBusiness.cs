using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBusiness:ICollabBusiness
    {
        private readonly ICollabRepository collabRepository;
        public CollabBusiness(ICollabRepository collabRepository)
        {
            this.collabRepository = collabRepository;
        }

        public CollabEntity AddCollaborator(string collabEmail, long noteId, long userId)
        {
            try
            {
                return collabRepository.AddCollaborator(collabEmail, noteId, userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public List<CollabEntity> GetCollabs(long noteId)
        {
            try
            {
                return collabRepository.GetCollabs(noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public bool DeleteCollab(long noteId, long userId)
        {
            try
            {
                return collabRepository.DeleteCollab(noteId, userId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
