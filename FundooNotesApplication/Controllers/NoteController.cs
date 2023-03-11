using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class NoteController : ControllerBase
    {
        private readonly INoteBusiness notesBusiness;

        public NoteController(INoteBusiness notesBusiness)
        {
            this.notesBusiness = notesBusiness;
        }

        //Adding notes
        [Authorize]
        [HttpPost("AddNotes")]
        public IActionResult AddNotes(AddNoteModel note)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
                var createNotes = notesBusiness.AddNotes(note, userId);
                if (createNotes != null)
                {
                    return Ok(new ResponseModel<NoteEntity> { Status = true, Message = "Note added successfully", Data = createNotes });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message = "Note adding failed", Data = null });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //Fetching all noetes of perticular user
        [Authorize]
        [HttpGet("GetAllNotes")]
        public ActionResult GetAllNotes()
        {
            try
            {
                long userId=Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
                var result=notesBusiness.GetAllNotes(userId);
                if(result != null)
                {
                    return Ok(new ResponseModel<List<NoteEntity> >{ Status = true, Message = "Getting all notes succeded", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<List<NoteEntity> >{ Status = false, Message = "Getting all notes failed", Data = null });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //Update existing note

        [Authorize]
        [HttpPut("UpdateNote")]
        public ActionResult UpdateNote(UpdateNoteModel updateNoteModel,long noteId)
        {
            try
            {
                long userId=Convert.ToInt32(User.Claims.FirstOrDefault(a=>a.Type=="UserId").Value);
                var result = notesBusiness.UpdateNote(updateNoteModel, noteId, userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<NoteEntity> { Status = true, Message = "update notes succeded", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message = "update failed", Data = null });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteNote")]
        public ActionResult DeleteNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
                bool result = notesBusiness.DeleteNote(noteId, userId);
                if (result == true)
                {
                    return Ok(new ResponseModel<bool> { Status = true, Message = "Note Deleted Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { Status = false, Message = "Note Deletion failed", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Pin or Unpin
        [Authorize]
        [HttpPut("PinOrUnpinNote")]
        public ActionResult PinOrPinNote(long noteId)
        {
            long userId=Convert.ToInt32(User.Claims.FirstOrDefault(a=>a.Type=="UserId").Value);
            var result=notesBusiness.PinOrUnpinNote(userId, noteId);
            if(result == true)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Note Pinned Successfully", Data = result });
            }
            else
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Note Uninned Successfully", Data = result });
            }
        }

        //Trash or Untrash
        [Authorize]
        [HttpPut("TrashOrUntrash")]
        public ActionResult TrashOrUntrash(long noteId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
            var result = notesBusiness.TrashOrUntrash(userId, noteId);
            if (result == true)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Note Trashed Successfully", Data = result });
            }
            else
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Note Untrashed Successfully", Data = result });
            }
        }

        //Archive or Unarchive
        [Authorize]
        [HttpPut("ArchiveOrUnarchive")]
        public ActionResult ArchiveOrUnarchive(long noteId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
            var result = notesBusiness.ArchiveOrUnarchive(userId, noteId);
            if (result == true)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Note Archived Successfully", Data = result });
            }
            else
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Note UnArchived Successfully", Data = result });
            }
        }
    }
}
