using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class VMMailInbox
    {
        public VMMailInbox()
        {
            Mails = new List<Mail>();
        }

        public List<Mail> Mails { get; set; }
    }
}
