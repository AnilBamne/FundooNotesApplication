using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BusinessLayer.Service
{
    public class NoteBusiness:INoteBusiness
    {
        private readonly INoteRepository notesRepository;
        public NoteBusiness(INoteRepository notesRepository)
        {
            this.notesRepository = notesRepository;
        }

        //Adding notes
        public NoteEntity AddNotes(AddNoteModel model, long userId)
        {
            try
            {
                return notesRepository.AddNotes(model, userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //retriving all notes
        public List<NoteEntity> GetAllNotes(long userId)
        {
            try
            {
                return notesRepository.GetAllNotes(userId);
            }
            catch(Exception ex)
            {
                throw ex ;
            }
        }
        public NoteEntity UpdateNote(UpdateNoteModel updateNoteModel, long noteId, long userId)
        {
            try
            {
                return notesRepository.UpdateNote(updateNoteModel, noteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public bool DeleteNote(long noteId, long userId)
        {
            try
            {
                return notesRepository.DeleteNote(noteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool PinOrUnpinNote(long userId, long noteId)
        {
            try
            {
                return notesRepository.PinOrUnpinNote(userId, noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool TrashOrUntrash(long userId, long noteId)
        {
            try
            {
                return notesRepository.TrashOrUntrash(userId, noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ArchiveOrUnarchive(long userId, long noteId)
        {
            try
            {
                return notesRepository.ArchiveOrUnarchive(userId, noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
