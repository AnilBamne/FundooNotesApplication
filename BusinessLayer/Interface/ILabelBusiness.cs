using CommonLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBusiness
    {
        public LabelEntity AddLabel(string labelName, long noteId, long userId);
        //public List<LabelEntity> GetAllLabels(long labelId);
        public IEnumerable<LabelEntity> GetAllLabels(long labelId);
        public LabelEntity UpdateLabel(UpdateLabelModel updateLabelModel, long noteId, long userId);
        public bool DeleteLabel(long labelId, long userId);
        public LabelEntity AddExistingLabel(long userId, long noteId, long labelId);
    }
}
