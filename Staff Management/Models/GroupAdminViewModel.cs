using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Staff_Management.Models
{
    public class GroupAdminViewModel
    {
        public string TaskTitle { get; set; }
        public byte TaskStatus { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }

        public List<GroupAdminViewModel> ListTasks()
        {
            using (var ctx = new DbContextViewModel())
            {
                var query = from task in ctx.Tasks
                            join user in ctx.Users
                            on task.StaffId equals user.UserId
                            select new GroupAdminViewModel
                            {
                                TaskStatus = task.Status,
                                TaskTitle = task.Title,
                                UserName = user.Name,
                                UserSurname = user.Surname
                            };

                return query.ToList();
            }
        }

    }
}