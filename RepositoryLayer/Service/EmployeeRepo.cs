using Microsoft.Extensions.Configuration;
using RepositoryLayer.DataBaseContext;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class EmployeeRepo: IEmployeeRepo
    {
        private readonly IConfiguration configuration;
        private readonly FundooContext fundooContext;
        public EmployeeRepo(IConfiguration configuration,FundooContext fundooContext)
        {
            this.configuration = configuration;
            this.fundooContext = fundooContext;
        }

        public EmpEntity AddEmp(string empName,string projName,int hrsWorked)
        {
            try
            {
                EmpEntity emp = new EmpEntity();
                emp.EmpName = empName;
                emp.ProjectName=projName;
                emp.HoursWorked = hrsWorked;
                fundooContext.Add(emp);
                fundooContext.SaveChanges();
                return emp;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
