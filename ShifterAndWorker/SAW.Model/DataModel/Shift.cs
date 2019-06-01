using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.DataModel
{
    public class Shift
    {
        public int ShiftId { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime CreateDate { get; set; }
        public int DepartmentId { get; set; }
        public int UserId { get; set; }
        public bool IsNotify { get; set; }
        public bool IsActive { get; set; }
        public int StoreId { get; set; }
    }
}
