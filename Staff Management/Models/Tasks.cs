using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Staff_Management.Models
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }
        public String Title { get; set; }
        public String Contents { get; set; }
        public String Comment { get; set; }
        public String Status { get; set; }
        public int StaffId { get; set; }
    }
}