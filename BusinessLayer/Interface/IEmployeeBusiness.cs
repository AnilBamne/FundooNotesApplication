using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IEmployeeBusiness
    {
        public EmpEntity AddEmp(string empName, string projName, int hrsWorked);
    }
}
