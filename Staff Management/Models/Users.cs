using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Staff_Management.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Title { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public int Type { get; set; }
        public float Score { get; set; }
        public float Salary { get; set; }
    }
}