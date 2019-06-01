using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class VMAnnouncementEdit
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsImportant { get; set; }

        public static VMAnnouncementEdit Parse(Announcement announcement)
        {
            VMAnnouncementEdit result = new VMAnnouncementEdit
            {
                AnnouncementId = announcement.AnnouncementId,
                Title = announcement.Title,
                Body = announcement.Body,
                IsImportant = announcement.IsImportant
            };

            return result;
        }
    }
}
