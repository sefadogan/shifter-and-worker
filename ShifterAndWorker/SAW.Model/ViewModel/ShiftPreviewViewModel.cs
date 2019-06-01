using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class ShiftPreviewViewModel
    {
        public ShiftPreviewViewModel()
        {
            PartialHours = new List<PartialHour>();
        }
        public List<PartialHour> PartialHours{ get; set; }
        public List<User> Users { get; set; }
    }
}
