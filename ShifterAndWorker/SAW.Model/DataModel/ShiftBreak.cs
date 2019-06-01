using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.DataModel
{
    public class ShiftBreak
    {
        public int ShiftBreakId { get; set; }
        public int BreakId { get; set; }
        public int ShiftId { get; set; }
    }
}
