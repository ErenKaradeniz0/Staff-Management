using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Staff_Management.Models
{
    public class AssignStaffViewModel
    {
        public List<Users> AdminList { get; set; }
        public List<Users> StaffList { get; set; }

        public AssignStaffViewModel()
        {
            AdminList = new List<Users>();
            StaffList = new List<Users>();
        }

    }
}