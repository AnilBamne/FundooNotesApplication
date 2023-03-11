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

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBusiness collabBusiness;
        public CollabController(ICollabBusiness collabBusiness)
        {
            this.collabBusiness = collabBusiness;
        }
        [Authorize]
        [HttpPost("AddCollaborator")]
        public ActionResult AddCollaborator(string collabEmail,long noteId) 
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
                var result = collabBusiness.AddCollaborator(collabEmail, noteId, userId);
                if(result != null)
                {
                    return Ok(new ResponseModel<CollabEntity> { Status = true, Message = "Collaborator Added Successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<CollabEntity> { Status = false, Message = "Collaborator Adding Failes" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //Get all collabs
        [Authorize]
        [HttpGet("GetCollabs")]
        public ActionResult GetCollabs(long noteId)
        {
            try
            {
                var result = collabBusiness.GetCollabs(noteId);
                if( result != null)
                {
                    return Ok(new ResponseModel<List<CollabEntity>> { Status = true, Message = "Retrived all collabs successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<List<CollabEntity>> { Status = false, Message = "Retrive Failed", Data = null });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpDelete("DeleteCollab")]
        public ActionResult DeleteCollab(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
                bool result = collabBusiness.DeleteCollab(noteId, userId);
                if (result == true)
                {
                    return Ok(new ResponseModel<bool> { Status = true, Message = "Collab Deleted Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { Status = false, Message = "Collab Deletion failed", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
