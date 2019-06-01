using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class VMShiftCreate
    {
        public int ShiftId { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public string BreakStartTime { get; set; }
        public string BreakEndTime { get; set; }

        public List<User> Users { get; set; }
        public List<Department> Departments { get; set; }

        public static VMShiftCreate Parse(List<User> users, List<Department> departments)
        {
            VMShiftCreate result = new VMShiftCreate
            {
                Users = users,
                Departments = departments
            };

            return result;
        }
    }
}
