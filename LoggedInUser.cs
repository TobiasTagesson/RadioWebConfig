using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RadioWebConfig
{
    public class LoggedInUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; } // 0 for customer 1 for admin
    }
}