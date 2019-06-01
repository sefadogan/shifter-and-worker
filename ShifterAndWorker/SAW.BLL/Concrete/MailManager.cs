using SAW.BLL.Abstract;
using SAW.BLL.Utilities.Log4net;
using SAW.Core;
using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SAW.BLL.Concrete
{
    public class MailManager : BaseManager<Mail, int, ShifterAndWorkerEntities>, IMailManager
    {
        private ShifterAndWorkerEntities _context;

        public MailManager(ShifterAndWorkerEntities context) : base(context)
        {
            _context = context;
        }

        public AppReturn Delete(int id)
        {
            try
            {
                Mail mail = _context.Mails.Find(id);
                mail.IsActive = false;
                mail.DeleteDate = DateTime.Now;

                Log.Info("Successfully deleted email.");
                return AppReturn.Successful("Mail deleted successfully.");
            }
            catch (Exception e)
            {
                Log.Error("An unexpected error has occured while deleting email.", e);
                return AppReturn.InvalidOperation("An error has occured when deleting email.");
            }
        }
        public AppReturn PermanentDelete(int id)
        {
            try
            {
                Mail mail = _context.Mails.Find(id);
                mail.IsPermanentDelete = true;
                return AppReturn.Successful("Successfully deleted mail permanently");
            }
            catch (Exception)
            {
                // TODO : Logger has to add here.
                return AppReturn.InvalidOperation("An error has occured when deleting email.");
            }
        }
        public Mail BringById(Expression<Func<Mail, bool>> filter)
        {
            return _context.Mails.Where(filter).FirstOrDefault();
        }
    }
}
