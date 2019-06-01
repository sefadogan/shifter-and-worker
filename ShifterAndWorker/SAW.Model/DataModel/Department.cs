using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.DataModel
{
    public class Department
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
    }
}
