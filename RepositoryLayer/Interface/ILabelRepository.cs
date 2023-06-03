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
        //public List<LabelEntity> GetAllLabels(long labelId);
        public IEnumerable<LabelEntity> GetAllLabels(long userId);
        public string UpdateLabel(long labelId, string newLabelName);
        public bool DeleteLabel(long labelId);
        public LabelEntity AddExistingLabel(long userId, long noteId, long labelId);
        public List<NoteEntity> GetNoteByLabel(string labelName, long userId);
    }
}
