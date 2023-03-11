using Microsoft.Extensions.Configuration;
using RepositoryLayer.DataBaseContext;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollabRepository:ICollabRepository
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public CollabRepository(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public CollabEntity AddCollaborator(string email,long noteId, long userId)
        {
            try
            {
                CollabEntity collabEntity = new CollabEntity();
                collabEntity.CollabEmail = email;
                collabEntity.NoteId = noteId;
                collabEntity.UserId = userId;
                fundooContext.Add(collabEntity);
                fundooContext.SaveChanges();
                return collabEntity;
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
                var collabList = fundooContext.CollabTable.Where(a => a.NoteId == noteId).ToList();
                return collabList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //delete collab
        public bool DeleteCollab(long noteId, long userId)
        {
            try
            {
                var getUser = fundooContext.CollabTable.Where(a => a.UserId == userId);
                if (getUser != null)
                {
                    var findNote = getUser.FirstOrDefault(a => a.NoteId == noteId);
                    if (findNote != null)
                    {
                        //if the note is present we r removing its collab from table
                        fundooContext.CollabTable.Remove(findNote);
                        fundooContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
