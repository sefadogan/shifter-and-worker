using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class VMAnnouncementDetail
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsImportant { get; set; }
        public User CreatorUser { get; set; }

        public static VMAnnouncementDetail Parse(Announcement announcement, User creator)
        {
            VMAnnouncementDetail result = new VMAnnouncementDetail
            {
                AnnouncementId = announcement.AnnouncementId,
                Title = announcement.Title,
                Body = announcement.Body,
                IsImportant = announcement.IsImportant,
                CreatorUser = creator
            };

            return result;
        }
    }
}
