using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel.Partials
{
    public class VMQuickViewSideBarPartial
    {
        public VMQuickViewSideBarPartial()
        {
            Notes = new List<VMNote>();
        }
        public List<VMNote> Notes { get; set; }
    }
}
