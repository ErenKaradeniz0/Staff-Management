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
        public byte Type { get; set; }
        public double Score { get; set; }
        public double Salary { get; set; }
    }
}