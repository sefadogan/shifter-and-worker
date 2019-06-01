using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.DataModel
{
    public class Announcement
    {

        public int AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsImportant { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatorId { get; set; }
    }
}
