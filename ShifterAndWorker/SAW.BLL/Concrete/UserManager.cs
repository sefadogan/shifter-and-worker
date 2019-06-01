using SAW.BLL.Abstract;
using SAW.BLL.Utilities.Log4net;
using SAW.Core;
using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SAW.BLL.Concrete
{
    public class UserManager : BaseManager<User, int, ShifterAndWorkerEntities>, IUserManager
    {
        readonly ShifterAndWorkerEntities _context;
        public UserManager(ShifterAndWorkerEntities context) : base(context)
        {
            _context = context;
        }

        public AppReturn Delete(int id)
        {
            try
            {
                User user = _context.Users.Find(id);
                user.IsActive = false;

                Log.Info("Successfully deleted user.");
                return AppReturn.Successful("User deleted successfully.");
            }
            catch (Exception e)
            {
                Log.Error("An unexpected error has occured while deleting user.", e);
                return AppReturn.InvalidOperation("An error has occured when deleting user.");
            }
        }
    }
}
