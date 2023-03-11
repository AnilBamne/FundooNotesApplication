using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.DataBaseContext;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRepository:ILabelRepository
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public LabelRepository(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        //Add label
        public LabelEntity AddLabel(string labelName,long noteId,long userId)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();
                
                labelEntity.LabelName= labelName;
                labelEntity.UserId = userId;
                labelEntity.NoteId= noteId;
                fundooContext.LableTable.Add(labelEntity);
                fundooContext.SaveChanges();
                return labelEntity;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //Get all
        public List<LabelEntity> GetAllLabels(long userId)
        {
            try
            {
                var labelList = fundooContext.LableTable.Where(l => l.UserId == userId).ToList();
                return labelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //update label
        public LabelEntity UpdateLabel(UpdateLabelModel updateLabelModel,long noteId,long userId)
        {
            try
            {
                var findUser=fundooContext.LableTable.Where(l => l.UserId == userId).FirstOrDefault();
                if (findUser != null)
                {
                    var findNote=fundooContext.LableTable.Where(l => l.NoteId == noteId).FirstOrDefault();
                    if(findNote != null)
                    {
                        findNote.LabelName= updateLabelModel.LabelName;
                        fundooContext.SaveChanges();
                        return findNote;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //delete label
        public bool DeleteLabel(long noteId, long userId)
        {
            try
            {
                var getUser = fundooContext.LableTable.Where(a => a.UserId == userId);
                if (getUser != null)
                {
                    var findNote = getUser.FirstOrDefault(a => a.NoteId == noteId);
                    if (findNote != null)
                    {
                        //if the note is present we r removing it from table
                        fundooContext.LableTable.Remove(findNote);
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
