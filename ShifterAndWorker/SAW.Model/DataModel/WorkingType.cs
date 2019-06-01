using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.DataModel
{
    public class WorkingType
    {
        public int WorkingTypeId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public byte MaxWorkingHours { get; set; }
        public byte MinWorkingHours { get; set; }
        public int StoreId { get; set; }
    }
}
