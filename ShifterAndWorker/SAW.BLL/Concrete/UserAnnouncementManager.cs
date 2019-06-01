using SAW.BLL.Abstract;
using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.BLL.Concrete
{
    public class UserAnnouncementManager : BaseManager<UserAnnouncement, int, ShifterAndWorkerEntities>, IUserAnnouncementManager
    {
        public UserAnnouncementManager(ShifterAndWorkerEntities context) : base(context)
        {
        }
    }
}
