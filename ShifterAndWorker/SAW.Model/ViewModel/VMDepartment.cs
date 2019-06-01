using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class VMDepartment
    {
        public int DepartmentId { get; set; }
        public int? ParentDepartmentId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public byte RootLevel { get; set; }
        public bool IsActive { get; set; }
        public int? MaxEmployees { get; set; }
        public short? MinEmployees { get; set; }
        public int StoreId { get; set; }

        public static VMDepartment Parse(Department model)
        {
            VMDepartment result = new VMDepartment
            {
               DepartmentId = model.DepartmentId,
               ParentDepartmentId = model.ParentDepartmentId,
               Name = model.Name,
               CreateDate = model.CreateDate,
               RootLevel = model.RootLevel,
               IsActive = model.IsActive,
               MaxEmployees = model.MaxEmployees,
               MinEmployees = model.MinEmployees,
               StoreId = model.StoreId
            };

            return result;
        }
        public static List<VMDepartment> Parse(List<Department> models)
        {
            List<VMDepartment> results = new List<VMDepartment>();

            foreach (var model in models)
                results.Add(VMDepartment.Parse(model));

            return results;
        }
    }
}
