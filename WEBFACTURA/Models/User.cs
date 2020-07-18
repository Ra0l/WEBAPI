using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBFACTURA.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        //Conventions
        public int UserTypeID { get; set; }
        public UserType UserType { get; set; }
    }
}