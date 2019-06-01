using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.DataModel
{
    public class UserShift
    {
        public int UserShiftId { get; set; }
        public int UserId { get; set; }
        public int ShiftId { get; set; }
        public bool IsAccepted { get; set; }
    }
}
