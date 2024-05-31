using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Staff_Management.Models
{
    public class GroupAdminViewModel
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public byte Status { get; set; }
        public String Contents { get; set; }
        public String Comment { get; set; }
        public int GroupAdminId { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string StaffSurname { get; set; }


        // Correctly define the ListTasks property
        public List<GroupAdminViewModel> ListTasks { get; set; }
        public List<GroupAdminViewModel> ListGroupStaff { get; set; }

        // Optionally, you could initialize the list in the constructor
        public GroupAdminViewModel()
        {

            ListTasks = new List<GroupAdminViewModel>();

            ListGroupStaff = new List<GroupAdminViewModel>();

        }


    }
}