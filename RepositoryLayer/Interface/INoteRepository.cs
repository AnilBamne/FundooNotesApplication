using CommonLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRepository
    {
        public NoteEntity AddNotes(AddNoteModel model, long userId);
        public List<NoteEntity> GetAllNotes(long userId);
        public NoteEntity UpdateNote(UpdateNoteModel updateNoteModel, long noteId, long userId);
        public bool DeleteNote(long noteId, long userId);
        public bool PinOrUnpinNote(long userId, long noteId);
        public bool TrashOrUntrash(long userId, long noteId);
        public bool ArchiveOrUnarchive(long userId, long noteId);
    }
}
