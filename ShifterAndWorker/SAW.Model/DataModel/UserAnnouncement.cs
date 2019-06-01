using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.DataModel
{
    public class UserAnnouncement
    {
        public int UserAnnouncementId { get; set; }
        public int UserId { get; set; }
        public int AnnouncementId { get; set; }
        public bool IsRead { get; set; }
    }
}
