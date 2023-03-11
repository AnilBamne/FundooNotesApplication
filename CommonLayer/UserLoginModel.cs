using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    //request model for Login
    public class UserLoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
