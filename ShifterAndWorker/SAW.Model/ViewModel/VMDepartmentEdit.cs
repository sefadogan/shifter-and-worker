using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class VMDepartmentEdit
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public short? MinEmployees{ get; set; }
        public int? MaxEmployees { get; set; }
        public int? ParentDepartmentId { get; set; }

        public List<Department> Departments { get; set; }

        public static VMDepartmentEdit Parse(Department department, List<Department> departments)
        {
            VMDepartmentEdit result = new VMDepartmentEdit
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                MinEmployees = department.MinEmployees,
                MaxEmployees = department.MaxEmployees,
                ParentDepartmentId = department.ParentDepartmentId,
                Departments = departments
            };

            return result;
        }
    }
}
