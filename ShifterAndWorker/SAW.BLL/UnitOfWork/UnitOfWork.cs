using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAW.BLL.Abstract;
using SAW.BLL.Concrete;
using SAW.DAL.Context;

namespace SAW.BLL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShifterAndWorkerEntities _context;
        private IUserManager userManager;
        private IRoleManager roleManager;
        private IMailManager mailManager;
        private IPostManager postManager;
        private ITaskManager taskManager;
        private IDepartmentManager departmentManager;
        private IWorkingTypeManager workingTypeManager;
        private IAnnouncementManager announcementManager;
        private IUserAnnouncementManager userAnnouncementManager;
        private IShiftManager shiftManager;
        private IBreakManager breakManager;
        private IShiftBreakManager shiftBreakManager;
        private INoteManager noteManager;

        public UnitOfWork(ShifterAndWorkerEntities context)
        {
            _context = context;
        }

        public IUserManager UserManager => userManager ?? (userManager = new UserManager(_context));
        public IRoleManager RoleManager => roleManager ?? (roleManager = new RoleManager(_context));
        public IMailManager MailManager => mailManager ?? (mailManager = new MailManager(_context));
        public IPostManager PostManager => postManager ?? (postManager = new PostManager(_context));
        public ITaskManager TaskManager => taskManager ?? (taskManager = new TaskManager(_context));
        public IDepartmentManager DepartmentManager => departmentManager ?? (departmentManager = new DepartmentManager(_context));
        public IWorkingTypeManager WorkingTypeManager => workingTypeManager ?? (workingTypeManager = new WorkingTypeManager(_context));
        public IAnnouncementManager AnnouncementManager => announcementManager ?? (announcementManager = new AnnouncementManager(_context));
        public IUserAnnouncementManager UserAnnouncementManager => userAnnouncementManager ?? (userAnnouncementManager = new UserAnnouncementManager(_context));
        public IShiftManager ShiftManager => shiftManager ?? (shiftManager = new ShiftManager(_context));
        public IBreakManager BreakManager => breakManager ?? (breakManager = new BreakManager(_context));
        public IShiftBreakManager ShiftBreakManager => shiftBreakManager ?? (shiftBreakManager = new ShiftBreakManager(_context));
        public INoteManager NoteManager => noteManager ?? (noteManager = new NoteManager(_context));

        public bool SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                return true; // AppReturn eklenebilir. 
            }
            catch (Exception)
            {
                // Logging must add here.
                return false;
            }
        }
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
