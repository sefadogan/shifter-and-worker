using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class VMAnnouncementList
    {
        public string UserRole { get; set; }
        public List<Announcement> Announcements { get; set; }

        public static VMAnnouncementList Parse(List<Announcement> announcements, User loggedUser)
        {
            VMAnnouncementList result = new VMAnnouncementList
            {
                Announcements = announcements,
                UserRole = loggedUser.Role.Name
            };

            return result;
        }
        public static VMAnnouncementList Parse(List<UserAnnouncement> userAnnouncements, User loggedUser)
        {
            VMAnnouncementList result = new VMAnnouncementList
            {
                Announcements = userAnnouncements.Select(x => x.Announcement).ToList(),
                UserRole = loggedUser.Role.Name
            };

            return result;
        }
    }
}
