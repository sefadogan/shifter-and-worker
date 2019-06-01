using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel.Partials
{
    public class VMSideBarPartial
    {
        public VMSideBarPartial()
        {
            User = new User();
        }

        public int NumberOfMails { get; set; }
        public User User { get; set; }
    }
}
