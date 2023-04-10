using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Entity;
using System;
using System.Linq;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        private readonly ILogger<UserController> logger;
        const string UserName = "UserName";
        const string UserEmail = "UserEmail";
        public UserController(IUserBusiness userBusiness,ILogger<UserController> logger)
        {
            this.userBusiness = userBusiness;
            this.logger = logger;
        }

        [HttpPost("Register")]      //this is controller
        public ActionResult UserRegister(UserRegistrationRequestModel requestModel)
        {
            try
            {
                HttpContext.Session.SetString(UserName, requestModel.FirstName);
                HttpContext.Session.SetString(UserEmail, requestModel.Email);
                var result = userBusiness.UserRegister(requestModel);
                if (result != null)
                {
                    var name=HttpContext.Session.GetString(UserName);
                    var email = HttpContext.Session.GetString(UserEmail);
                    logger.LogInformation("Registratin Proccess was successfull");
                    return Ok(new ResponseModel<UserEntity> { Status = true, Message = "Registered succesfully", Data = result });
                }
                else
                {
                    logger.LogWarning("Registration failed, try again with propper inputs");
                    return BadRequest(new ResponseModel<UserEntity> { Status = false, Message = "Registeration failed", Data = result });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("Login")]     //this is controller
        public ActionResult UserLogin(UserLoginRequestModel loginModel)
        {
            try
            {
                UserRegistrationRequestModel data = new UserRegistrationRequestModel();
                var result=userBusiness.UserLogin(loginModel);
                if(result != null)
                {
                    //SetSession(data);
                    //var name=HttpContext.Session.GetString("UserName");
                    //var email = HttpContext.Session.GetString("UserEmail");
                    return Ok(new ResponseModel<string> { Status=true,Message="Login succesfull",Data=result});
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Status = false, Message = "Login failed", Data = result });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("ForgetPassword")]
        public ActionResult ForgetPassword(string email)
        {
            try
            {
                var result = userBusiness.ForgetPassword(email);
                if (result != null)
                {
                    return Ok(new ResponseModel<string> { Status = true, Message = "Reset link sent to your registered email, Set new password", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Status = false, Message = "Email not found", Data = result });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]
        public ActionResult ResetPassword(ResetPasswordModel reset)
        {
            try
            {
                string email = User.Claims.FirstOrDefault(o => o.Type == "Email").Value;
                var result = userBusiness.ResetPassword(reset, email);
                if (result != null)
                {
                    return Ok(new ResponseModel<string> { Status = true, Message = "Reset password successfull", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Status = false, Message = "Reset password not successfull", Data = result });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //public void SetSession(UserRegistrationRequestModel model)
        //{
        //    HttpContext.Session.SetString("UserName", model.FirstName);
        //    HttpContext.Session.SetString("UserEmail", model.Email);
        //    //HttpContext.Session.SetInt32("UserId", model.UserId);         //not available
        //}
    }
}
