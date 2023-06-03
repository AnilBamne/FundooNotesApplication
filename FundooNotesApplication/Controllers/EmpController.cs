using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        private readonly IEmployeeBusiness employeeBusiness;
        public EmpController(IEmployeeBusiness employeeBusiness)
        {
            this.employeeBusiness = employeeBusiness;
        }

        [HttpPost("AddEmployee")]
        public ActionResult AddEmployee(string name,string projName,int hrsWorked)
        {
            try
            {
                var result=this.employeeBusiness.AddEmp(name,projName,hrsWorked);
                if (result != null)
                {
                    return Ok(new ResponseModel<EmpEntity> { Status = true, Message = "Employee Added Succesfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<EmpEntity> { Status = false, Message = "Employee Adding Failed", Data = result });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
