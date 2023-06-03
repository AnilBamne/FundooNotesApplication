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
        //public LabelEntity AddLabel(string labelName, long noteId, long userId)
        //{
        //    try
        //    {
        //        var userResult = this.fundooContext.UserDataTable.Where(e => e.UserId == userId).FirstOrDefault();
        //        var noteResult = this.fundooContext.NoteDataTable.Where(e => e.NoteId == noteId).FirstOrDefault();
        //        if (userResult != null && noteResult != null)
        //        {
        //            LabelEntity labelEntity = new LabelEntity();

        //            labelEntity.LabelName = labelName;
        //            labelEntity.UserId = userId;
        //            labelEntity.NoteId = noteId;
        //            fundooContext.LableTable.Add(labelEntity);
        //            fundooContext.SaveChanges();
        //            return labelEntity;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

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

        public IEnumerable<LabelEntity> GetAllLabels(long userId)
        {
            try
            {
                var result = fundooContext.LableTable.Where(l => l.UserId == userId).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //update label
        public string UpdateLabel(long labelId,string newLabelName)
        {
            try
            {
                var findLabel=fundooContext.LableTable.FirstOrDefault(a=>a.LabelId==labelId);
                if (findLabel != null)
                {
                    findLabel.LabelName= newLabelName;
                    fundooContext.SaveChanges();
                    return "Label name updated";
                }
                else
                {
                    return "Label Id Not Found";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //delete label
        public bool DeleteLabel(long labelId)
        {
            try
            {
                var result = fundooContext.LableTable.FirstOrDefault(a => a.LabelId == labelId);
                if (result != null)
                {
                    fundooContext.LableTable.Remove(result);
                    fundooContext.SaveChanges();
                    return true;
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

        //public LabelEntity newAddExistingLabel(long userId, long noteId, long labelId)
        //{
        //    var result = fundooContext.NoteDataTable.FirstOrDefault(a => a.NoteId == noteId);
        //    if (result != null)
        //    {
        //        var label = fundooContext.LableTable.FirstOrDefault(a => a.LabelId == labelId);
        //        if (label != null)
        //        {
                    
        //            label.NoteId = noteId;
        //            label.UserId = userId;
        //            fundooContext.SaveChanges();
        //            return label;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public string AddAvailabelLabel(string label,long noteId,long userId)
        //{
        //    try
        //    {
        //        //var note = this.fundooContext.NoteDataTable.FirstOrDefault(a => a.NoteId == noteId);
        //        var result=fundooContext.LableTable.Where(a=>a.LabelName!=label).FirstOrDefault();
        //        if(result != null)
        //        {
        //            LabelEntity le = new LabelEntity();
        //            le.LabelName = label;
        //            le.NoteId = noteId;
        //            le.UserId=userId;
        //            //le.Note = note;
        //            fundooContext.LableTable.Add(le);
        //            fundooContext.SaveChanges();
        //            return "label added";
        //        }
        //        else
        //        {
        //            return "label not added";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        public List<NoteEntity> GetNoteByLabel( string labelName,long userId)
        {
            
            var result = this.fundooContext.LableTable.FirstOrDefault(a =>a.UserId==userId && a.LabelName == labelName);
            if(result != null)
            {
                long noteId = result.NoteId;
                var note = this.fundooContext.NoteDataTable.Where(a => a.NoteId == noteId).ToList();
                if(note != null)
                {
                    return note;
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
