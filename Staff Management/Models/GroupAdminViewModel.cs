using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace Staff_Management.Models
{
    public class GroupAdminViewModel
    {
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public byte TaskStatus { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }

        // Correctly define the ListTasks property
        public List<GroupAdminViewModel> ListTasks { get; set; }

        // Optionally, you could initialize the list in the constructor
        public GroupAdminViewModel()
        {
            ListTasks = new List<GroupAdminViewModel>();
        }

    }
}