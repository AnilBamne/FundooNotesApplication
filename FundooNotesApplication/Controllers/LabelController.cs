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
                    return Ok(new ResponseModel<List<LabelEntity>> { Status=true,Message="Getting All Labels Succesfull",Data = result});
                }
                else
                {
                    return BadRequest(new ResponseModel<List<LabelEntity>> { Status = false, Message = "Getting All Labels Failed", Data = null }); ;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("UpdateLabel")]
        public ActionResult UpdateLabel(UpdateLabelModel updateLabelModel, long noteId)
        {
            try
            {
                long userId=Convert.ToInt32(User.Claims.FirstOrDefault(a=>a.Type=="UserId").Value);
                var result = labelBusiness.UpdateLabel(updateLabelModel, noteId, userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<LabelEntity> { Status = true, Message = "update label succeded", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<LabelEntity> { Status = false, Message = "update failed", Data = null });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpDelete("DeleteLabel")]
        public ActionResult DeleteLabel(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
                bool result = labelBusiness.DeleteLabel(noteId, userId);
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
    }
}
