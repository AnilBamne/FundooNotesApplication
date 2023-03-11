using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    //request model for Registration
    public class UserRegistrationRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
