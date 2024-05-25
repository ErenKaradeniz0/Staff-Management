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

        public List<GroupAdminViewModel> ListTasks()
        {
            using (var ctx = new DbContextViewModel())
            {
                var query = from assignment in ctx.Assignments
                            join user in ctx.Users on assignment.StaffId equals user.UserId
                            join task in ctx.Tasks on user.UserId equals task.StaffId
                            select new GroupAdminViewModel
                            {
                                TaskId = task.TaskId,
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