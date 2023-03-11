using CommonLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRepository
    {
        public LabelEntity AddLabel(string labelName, long noteId, long userId);
        public List<LabelEntity> GetAllLabels(long userId);
        public LabelEntity UpdateLabel(UpdateLabelModel updateLabelModel, long noteId, long userId);
        public bool DeleteLabel(long noteId, long userId);
    }
}
