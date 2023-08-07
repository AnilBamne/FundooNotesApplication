using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.DataBaseContext;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NoteRepository:INoteRepository
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public NoteRepository(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        //method
        public NoteEntity AddNotes(AddNoteModel model,long userId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title = model.Title;
                noteEntity.Description = model.Description;
                noteEntity.Color = model.Color;
                noteEntity.Reminder = model.Reminder;
                noteEntity.IsArchive = model.IsArchive;
                noteEntity.IsPinned = model.IsPinned;
                noteEntity.IsTrash = model.IsTrash;
                noteEntity.CreatedAt = model.CreatedAt;
                noteEntity.ModifiedAt = model.ModifiedAt;
                noteEntity.UserId = userId;
                fundooContext.NoteDataTable.Add(noteEntity);
                fundooContext.SaveChanges();
                return noteEntity;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //GetAllNotes method
        public List<NoteEntity> GetAllNotes(long userId)
        {
            try
            {
                var noteList=fundooContext.NoteDataTable.Where(a=>a.UserId==userId).ToList();
                return noteList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //Update existing note
        public NoteEntity UpdateNote(UpdateNoteModel updateNoteModel,long noteId,long userId)
        {
            try
            {
                var getUser = fundooContext.NoteDataTable.Where(a => a.UserId == userId);
                if (getUser != null)
                {
                    var findNote = getUser.FirstOrDefault(a => a.NoteId == noteId);
                    if (findNote != null)
                    {
                        findNote.Title = updateNoteModel.Title;
                        findNote.Description = updateNoteModel.Description;
                        findNote.Color = updateNoteModel.Color;
                        findNote.Reminder = updateNoteModel.Reminder;
                        findNote.IsArchive = updateNoteModel.IsArchive;
                        findNote.IsPinned = updateNoteModel.IsPinned;
                        findNote.IsTrash = updateNoteModel.IsTrash;
                        findNote.CreatedAt = updateNoteModel.CreatedAt;
                        findNote.ModifiedAt = DateTime.Now;
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
        //delete existing note
        public bool DeleteNote(long noteId, long userId)
        {
            try
            {
                var getUser = fundooContext.NoteDataTable.Where(a => a.UserId == userId);
                if (getUser != null)
                {
                    var findNote = getUser.FirstOrDefault(a => a.NoteId == noteId);
                    if (findNote != null)
                    {
                        //if the note is present we r removing it from table
                        fundooContext.NoteDataTable.Remove(findNote);
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

        //pin or unpin the note
        public bool PinOrUnpinNote(long userId,long noteId)
        {
            try
            {
                NoteEntity note = fundooContext.NoteDataTable.Where(a => a.NoteId == noteId).FirstOrDefault();
                if (note.IsPinned == false)
                {
                    note.IsPinned = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    note.IsPinned = false;
                    fundooContext.SaveChanges();
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //Trash or Untrash
        public bool TrashOrUntrash(long userId, long noteId)
        {
            try
            {
                NoteEntity note = fundooContext.NoteDataTable.Where(a => a.NoteId == noteId).FirstOrDefault();
                if (note.IsTrash == false)
                {
                    note.IsTrash = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    note.IsTrash = false;
                    fundooContext.SaveChanges();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Archive or unarchive
        public bool ArchiveOrUnarchive(long userId, long noteId)
        {
            try
            {
                NoteEntity note = fundooContext.NoteDataTable.Where(a => a.NoteId == noteId).FirstOrDefault();
                if (note.IsArchive == false)
                {
                    note.IsArchive = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    note.IsArchive = false;
                    fundooContext.SaveChanges();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public List<NoteEntity> GetNoteByKeawords(string input)
       {
            var result = fundooContext.NoteDataTable.Where(a => a.Description.Contains(input) || a.Title.Contains(input)).ToList();

            return result;
       }

        public string SetNoteColor(long userId,long noteId,string color)
        {
            NoteEntity note = fundooContext.NoteDataTable.Where(a => a.NoteId == noteId).FirstOrDefault();
            note.Color = color;
            fundooContext.SaveChanges();
            return note.Color;
        }
        public int CountNotes(long userId)
        {
            int count=fundooContext.NoteDataTable.Where(u=>u.UserId==userId).Count();
            return count;
        }
        public NoteCountModel CountAllNotes(long userId)
        {
            NoteCountModel model = new NoteCountModel();
            int AllNotescount = fundooContext.NoteDataTable.Where(u => u.UserId == userId).Count();
            int TrashedNotes=fundooContext.NoteDataTable.Where(a=>a.UserId==userId&&a.IsTrash==true).Count();
            int ArchivedNotes = fundooContext.NoteDataTable.Where(a => a.UserId == userId && a.IsArchive == true).Count();
            int ColouredNotes = fundooContext.NoteDataTable.Where(a => a.UserId == userId && a.Color !="string" && a.Color !=null).Count();
            model.TotalNotes = AllNotescount;
            model.TrashedNotes=TrashedNotes;
            model.ArchivedNotes=ArchivedNotes;
            model.ColouredNotes=ColouredNotes;
            return model;
        }
    }
}
