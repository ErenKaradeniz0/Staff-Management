using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Staff_Management.Models
{
    public class DaysOff
    {
        [Key]
        public int DoId { get; set; }
        public int UserId { get; set; }
        public int GroupAdminId { get; set; }

    }
}