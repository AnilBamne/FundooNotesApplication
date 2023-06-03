using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBusiness:ILabelBusiness
    {
        private readonly ILabelRepository labelRepository;
        public LabelBusiness(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }
        public LabelEntity AddLabel(string labelName, long noteId, long userId)
        {
            try
            {
                return labelRepository.AddLabel(labelName, noteId, userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<LabelEntity> GetAllLabels(long userId)
        {
            try
            {
                return labelRepository.GetAllLabels(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string UpdateLabel(long labelId, string newLabelName)
        {
            try
            {
                return labelRepository.UpdateLabel(labelId, newLabelName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteLabel(long labelId)
        {
            try
            {
                return labelRepository.DeleteLabel(labelId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LabelEntity AddExistingLabel(long userId, long noteId, long labelId)
        {
            return labelRepository.AddExistingLabel(userId, noteId, labelId);
        }
        public List<NoteEntity> GetNoteByLabel(string labelName, long userId)
        {
            return labelRepository.GetNoteByLabel(labelName, userId);
        }
    }
}
