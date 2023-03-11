using CommonLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        public UserEntity UserRegister(UserRegistrationRequestModel requestModel);
        public string EncryptPassword(string password);
        public string UserLogin(UserLoginRequestModel loginModel);
        public string ForgetPassword(string email);
        public string ResetPassword(ResetPasswordModel reset, string email);
        public UserTicketModel CreateTicketForPassword(string emailId, string token);
    }
}
