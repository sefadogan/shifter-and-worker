using SAW.BLL.Abstract;
using SAW.BLL.Utilities.Log4net;
using SAW.Core;
using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.BLL.Concrete
{
    public class AnnouncementManager : BaseManager<Announcement, int, ShifterAndWorkerEntities>, IAnnouncementManager
    {
        private ShifterAndWorkerEntities _context;

        public AnnouncementManager(ShifterAndWorkerEntities context) : base(context)
        {
            _context = context;
        }

        public AppReturn Delete(int id)
        {
            try
            {
                Announcement announcement = _context.Announcements.Find(id);
                announcement.IsActive = false;

                Log.Info("Successfully deleted announcement.");
                return AppReturn.Successful("Announcement deleted successfully.");
            }
            catch (Exception e)
            {
                Log.Error("An unexpected error has occured while deleting announcement.", e);
                return AppReturn.InvalidOperation("An error has occured when deleting announcement.");
            }
        }
    }
}
