using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Staff_Management.Models
{
    public class Assignments
    {
        [Key]
        public int AssignId { get; set; }
        public int GroupAdminId { get; set; }
        public int StaffId { get; set; }
    }
}