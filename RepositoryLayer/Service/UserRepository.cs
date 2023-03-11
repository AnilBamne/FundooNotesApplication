using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.DataBaseContext;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace RepositoryLayer.Service
{
    public class UserRepository:IUserRepository
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public UserRepository(FundooContext fundooContext,IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        
        //method for encrypting password
        public string EncryptPassword(string password)
        {
            try
            {
                var passwordTextForm = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(passwordTextForm);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // method for user registration
        public UserEntity UserRegister(UserRegistrationRequestModel requestModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = requestModel.FirstName;
                userEntity.LastName = requestModel.LastName;
                userEntity.Email = requestModel.Email;
                //userEntity.Password = requestModel.Password;
                userEntity.Password = EncryptPassword(requestModel.Password);
                fundooContext.UserDataTable.Add(userEntity);
                fundooContext.SaveChanges();
                return userEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        //method for user login
        public string UserLogin(UserLoginRequestModel loginModel)
        {
            try
            {
                var currentUser = fundooContext.UserDataTable.FirstOrDefault(x => x.Email == loginModel.Email && x.Password == EncryptPassword(loginModel.Password));
                if (currentUser != null)
                {
                    var token = GenerateToken(loginModel.Email, currentUser.UserId);
                    return token;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //GenerateToken 
        private string GenerateToken(string emailId,long userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",emailId),
                new Claim("UserId",userId.ToString())
            };
            var token = new JwtSecurityToken(
                issuer:configuration["Jwt:Issuer"],
                audience:configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ForgetPassword(string email)
        {
            var emailCheck = this.fundooContext.UserDataTable.Where(b => b.Email == email).FirstOrDefault();
            if(emailCheck != null)
            {
                var token = GenerateToken(emailCheck.Email, emailCheck.UserId);
                new MSMQ().SendMessage(token, emailCheck.Email, emailCheck.FirstName);
                return token;
            }
            else
            {
                return null;
            }
        }

        public string ResetPassword(ResetPasswordModel reset, string email)
        {
            if (reset.Password.Equals(reset.ConfirmPassword))
            {
                var emailCheck = this.fundooContext.UserDataTable.Where(b => b.Email == email).FirstOrDefault();
                emailCheck.Password = EncryptPassword(reset.ConfirmPassword);
                fundooContext.SaveChanges();
                return "Reset Done";
            }
            else
            {
                return null;
            }
        }

        //Implementing rabbitMQ
        public UserTicketModel CreateTicketForPassword(string emailId, string token)
        {
            try
            {
                var userData = fundooContext.UserDataTable.FirstOrDefault(a => a.Email == emailId);
                if (userData != null)
                {
                    UserTicketModel userTicketModel = new UserTicketModel
                    {
                        FirstName = userData.FirstName,
                        LastName = userData.LastName,
                        EmailId = emailId,
                        Token = token,
                        IssueAt = DateTime.Now
                    };
                    return userTicketModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
