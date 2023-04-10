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
                var userResult = this.fundooContext.UserDataTable.Where(e => e.UserId == userId).FirstOrDefault();
                var noteResult = this.fundooContext.NoteDataTable.Where(e => e.NoteId == noteId).FirstOrDefault();
                if (userResult != null&& noteResult != null)
                {
                    LabelEntity labelEntity = new LabelEntity();

                    labelEntity.LabelName = labelName;
                    labelEntity.UserId = userId;
                    labelEntity.NoteId = noteId;
                    fundooContext.LableTable.Add(labelEntity);
                    fundooContext.SaveChanges();
                    return labelEntity;
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

        //Get all
        //public List<LabelEntity> GetAllLabels(long userId)
        //{
        //    try
        //    {
        //        var labelList = fundooContext.LableTable.Where(l => l.UserId == userId).ToList();
        //        return labelList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public IEnumerable<LabelEntity> GetAllLabels(long labelId)
        {
            try
            {
                var result = fundooContext.LableTable.Where(e => e.LabelId == labelId).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
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
        public bool DeleteLabel(long labelId, long userId)
        {
            try
            {
                var getUser = fundooContext.LableTable.Where(a => a.UserId == userId);
                if (getUser != null)
                {
                    var findLabel = getUser.FirstOrDefault(a => a.LabelId == labelId);
                    if (findLabel != null)
                    {
                        //if the note is present we r removing it from table
                        fundooContext.LableTable.Remove(findLabel);
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
        //add existing label
        public LabelEntity AddExistingLabel(long userId,long noteId,long labelId)
        {
            var result = fundooContext.NoteDataTable.FirstOrDefault(a => a.NoteId == noteId);
            if(result != null)
            {
                var label=fundooContext.LableTable.FirstOrDefault(a => a.LabelId == labelId);
                if(label != null)
                {
                    //LabelEntity entity = new LabelEntity();
                    //entity.LabelId=labelId;
                    //entity.UserId = userId;
                    //entity.NoteId = noteId;
                    //fundooContext.Add(entity);
                    //fundooContext.SaveChanges();
                    //return entity;
                    label.NoteId = noteId;
                    label.UserId = userId;
                    fundooContext.SaveChanges();
                    return label;
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
    }
}
