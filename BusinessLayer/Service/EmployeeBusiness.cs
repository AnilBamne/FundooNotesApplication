using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class EmployeeBusiness:IEmployeeBusiness
    {
        private readonly IEmployeeRepo employeeRepo;
        public EmployeeBusiness(IEmployeeRepo employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }

        public EmpEntity AddEmp(string empName, string projName, int hrsWorked)
        {
            try
            {
                return this.employeeRepo.AddEmp(empName, projName, hrsWorked);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
