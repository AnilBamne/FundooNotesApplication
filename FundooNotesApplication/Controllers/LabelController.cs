using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness labelBusiness;
        public LabelController(ILabelBusiness labelBusiness)
        {
            this.labelBusiness = labelBusiness;
        }

        [Authorize]
        [HttpPost("AddLabel")]
        public ActionResult AddLabel(string labelName,long noteId)
        {
            try
            {
                var userId=Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
                
                var result=labelBusiness.AddLabel(labelName,noteId,userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<LabelEntity> { Status = true, Message = "Label added successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<LabelEntity> { Status = false, Message = "Label adding failed", Data = null });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetAllLabel")]
        public ActionResult GetAllLabel()
        {
            try
            {
                long userId=Convert.ToInt32(User.Claims.FirstOrDefault(a=>a.Type=="UserId").Value);
                var result = labelBusiness.GetAllLabels(userId);
                if(result != null)
                {
                    return Ok(new ResponseModel<IEnumerable<LabelEntity>> { Status=true,Message="Getting All Labels Succesfull",Data = result});
                }
                else
                {
                    return BadRequest(new ResponseModel< IEnumerable<LabelEntity>> { Status = false, Message = "Getting All Labels Failed", Data = null }); ;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("UpdateLabel")]
        public ActionResult UpdateLabel(long labelId,string newLabelName)
        {
            try
            {
                var result = labelBusiness.UpdateLabel(labelId, newLabelName);
                if (result != null)
                {
                    return Ok(new ResponseModel<string> { Status = true, Message = "update label succeded", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Status = false, Message = "update failed", Data = result });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpDelete("DeleteLabel")]
        public ActionResult DeleteLabel(long labelId)
        {
            try
            {
                bool result = labelBusiness.DeleteLabel(labelId);
                if (result == true)
                {
                    return Ok(new ResponseModel<bool> { Status = true, Message = "Label Deleted Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { Status = false, Message = "Label Deletion failed", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost("AddExistingLabel")]
        public ActionResult AddExistingLabel(long labelId,long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);

                var result = labelBusiness.AddExistingLabel(labelId, noteId, userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<LabelEntity> { Status = true, Message = "Label added successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<LabelEntity> { Status = false, Message = "Label adding failed", Data = null });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetNoteByLabel")]
        public ActionResult GetNoteByLabel(string labelName)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
            var result = this.labelBusiness.GetNoteByLabel(labelName, userId);
            if (result != null)
            {
                return Ok(new ResponseModel<List<NoteEntity>> { Status = true, Message = "GetNoteByLabel Successfull", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<List<NoteEntity>> { Status = false, Message = "GetNoteByLabel Failed", Data = null }); ;
            }
        }
    }
}
