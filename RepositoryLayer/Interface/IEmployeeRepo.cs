using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IEmployeeRepo
    {
        public EmpEntity AddEmp(string empName, string projName, int hrsWorked);
    }
}
