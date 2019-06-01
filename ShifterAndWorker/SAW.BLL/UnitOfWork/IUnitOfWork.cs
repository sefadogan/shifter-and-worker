using SAW.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.BLL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserManager UserManager { get; }
        IRoleManager RoleManager { get; }
        IMailManager MailManager { get; }
        IPostManager PostManager { get; }
        ITaskManager TaskManager { get; }
        IDepartmentManager DepartmentManager { get; }
        IWorkingTypeManager WorkingTypeManager { get; }
        IAnnouncementManager AnnouncementManager { get; }
        IUserAnnouncementManager UserAnnouncementManager { get; }
        IShiftManager ShiftManager { get; }
        IBreakManager BreakManager { get; }
        IShiftBreakManager ShiftBreakManager { get; }
        INoteManager NoteManager { get; }

        bool SaveChanges();
    }
}
