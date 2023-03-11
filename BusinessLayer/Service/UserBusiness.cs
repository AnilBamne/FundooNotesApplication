using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
   
    public class UserBusiness:IUserBusiness
    {
        private readonly IUserRepository userRepository;
        public UserBusiness(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        //user registration
        public UserEntity UserRegister(UserRegistrationRequestModel requestModel)
        {
            return userRepository.UserRegister(requestModel);
        }

        //password encryption
        public string EncryptPassword(string password)
        {
            return userRepository.EncryptPassword(password);
        }

        //user login
        public string UserLogin(UserLoginRequestModel loginModel)
        {
            return userRepository.UserLogin(loginModel);
        }

        public string ForgetPassword(string email)
        {
            return userRepository.ForgetPassword(email);
        }

        public string ResetPassword(ResetPasswordModel reset, string email)
        {
            return userRepository.ResetPassword(reset, email);
        }

        public UserTicketModel CreateTicketForPassword(string emailId, string token)
        {
            return userRepository.CreateTicketForPassword(emailId, token);
        }
    }
}
