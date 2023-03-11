using BusinessLayer.Interface;
using CommonLayer;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Threading.Tasks;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IBus ibus;
        private readonly IUserBusiness userBusiness;
        public TicketController(IBus ibus, IUserBusiness userBusiness)
        {
            this.ibus = ibus;
            this.userBusiness = userBusiness;
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> CreateTicketForPassword(string emailId)
        {
            try
            {
                var token = userBusiness.ForgetPassword(emailId);
                if (!string.IsNullOrEmpty(token))
                {
                    var ticket = userBusiness.CreateTicketForPassword(emailId, token);
                    Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                    var endpoint = await ibus.GetSendEndpoint(uri);
                    await endpoint.Send(ticket);
                    return Ok(new ResponseModel<string> { Status = true, Message = "Mail sent successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Status = false, Message = "Email not found" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
