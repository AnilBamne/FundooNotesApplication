using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class NoteController : ControllerBase
    {
        private readonly INoteBusiness notesBusiness;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBusiness notesBusiness,IDistributedCache distributedCache)
        {
            this.notesBusiness = notesBusiness;
            this.distributedCache = distributedCache;
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
        //[Authorize]
        //[HttpGet("GetAllNotes")]
        //public ActionResult GetAllNotes()
        //{
        //    try
        //    {
        //        long userId=Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
        //        var result=notesBusiness.GetAllNotes(userId);
        //        if(result != null)
        //        {
        //            return Ok(new ResponseModel<List<NoteEntity> >{ Status = true, Message = "Getting all notes succeded", Data = result });
        //        }
        //        else
        //        {
        //            return BadRequest(new ResponseModel<List<NoteEntity> >{ Status = false, Message = "Getting all notes failed", Data = null });
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Implementing rediws cache.
        /// </summary>
        [Authorize]
        [HttpGet("GetAllNotes")]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {
                var cacheKey = $"noteList_{User.FindFirst("UserId").Value}"; //defining the key and value
                var serializedNoteList=await distributedCache.GetStringAsync(cacheKey);

                List<NoteEntity> noteList;
                if(serializedNoteList != null)
                {
                    noteList=JsonConvert.DeserializeObject<List<NoteEntity>>(serializedNoteList);
                }
                else
                {
                    long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
                    noteList = notesBusiness.GetAllNotes(userId);
                    serializedNoteList = JsonConvert.SerializeObject(noteList);
                    await distributedCache.SetStringAsync(
                        cacheKey, 
                        serializedNoteList,
                        new DistributedCacheEntryOptions { 
                            AbsoluteExpirationRelativeToNow=TimeSpan.FromMinutes(10),
                            SlidingExpiration=TimeSpan.FromMinutes(10)
                        });
                }
                if (noteList != null)
                {
                    return Ok(new ResponseModel<List<NoteEntity>> { Status = true, Message = "Getting all notes succeded", Data = noteList });
                }
                else
                {
                    return BadRequest(new ResponseModel<List<NoteEntity>> { Status = false, Message = "Getting all notes failed", Data = null });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update existing note.
        /// </summary>
        [Authorize]
        [HttpPut("UpdateNote")]
        public ActionResult UpdateNote(UpdateNoteModel updateNoteModel, long noteId)
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

        // Pin or Unpin
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

        // Trash or Untrash
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

        // Archive or Unarchive
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

        [Authorize]
        [HttpPut("SetNoteColor")]
        public ActionResult SetNoteColor(long noteId,string color)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
            var result = notesBusiness.SetNoteColor(userId, noteId,color);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Color applied to note Successfully", Data = result });
            }
            else
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Color application failed ", Data = result });
            }
        }

        /// <summary>
        /// search note by keyword.
        /// </summary>
        /// <param name="keyword">search keyword.</param>
        /// <returns>note.</returns>
        [Authorize]
        [HttpGet("GetNoteByKeyword")]
        public ActionResult SearchNote(string keyword)
        {
            var res = this.notesBusiness.GetNoteByKeawords(keyword);
            if (res != null)
            {
                return Ok(new ResponseModel<List<NoteEntity>> { Status = true, Message = "Note having keyword fount", Data = res });
            }
            else
            {
                return Ok(new ResponseModel<List<NoteEntity>> { Status = true, Message = "No such notes are available", Data = null });
            }
        }

        /// <summary>
        /// count.
        /// </summary>
        /// <returns>count.</returns>
        [Authorize]
        [HttpGet("CountNotes")]
        public ActionResult CountNotes()
        {
            long UserId=Convert.ToInt32(User.Claims.FirstOrDefault(a=>a.Type=="UserId").Value);
            var result=notesBusiness.CountNotes(UserId);
            if (result != 0)
            {
                return Ok(new ResponseModel<int> { Status = true,Message="Note counted successfully",Data=result });
            }
            else
            {
                return BadRequest(new ResponseModel<int> { Status = false, Message = "Note count failed", Data = result });
            }
        }

        /// <summary>
        /// counting notes of a perticular user.
        /// </summary>
        /// <returns> count. </returns>
        [Authorize]
        [HttpGet("CountAllNotes")]
        public ActionResult CountAllNotes()
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
            var result = notesBusiness.CountAllNotes(UserId);
            if (result != null)
            {
                return Ok(new ResponseModel<NoteCountModel> { Status = true, Message = "Notes counted successfully", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteCountModel> { Status = false, Message = "Notes count failed", Data = result });
            }
        }
    }
}
