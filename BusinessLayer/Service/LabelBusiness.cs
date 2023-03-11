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
        public List<LabelEntity> GetAllLabels(long userId)
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
        public LabelEntity UpdateLabel(UpdateLabelModel updateLabelModel, long noteId, long userId)
        {
            try
            {
                return labelRepository.UpdateLabel(updateLabelModel,noteId,userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteLabel(long noteId, long userId)
        {
            try
            {
                return labelRepository.DeleteLabel(noteId,userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
