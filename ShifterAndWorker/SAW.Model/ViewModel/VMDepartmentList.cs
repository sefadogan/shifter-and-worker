using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class VMDepartmentList
    {
        public List<Department> Departments { get; set; }

        public static VMDepartmentList Synchronize(List<Department> departments)
        {
            VMDepartmentList result = new VMDepartmentList
            {
                Departments = departments
            };

            return result;
        }
    }
}
