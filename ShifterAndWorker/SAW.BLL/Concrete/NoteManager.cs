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
    public class NoteManager : BaseManager<Note, int, ShifterAndWorkerEntities>, INoteManager
    {
        readonly ShifterAndWorkerEntities _context;

        public NoteManager(ShifterAndWorkerEntities context) : base(context)
        {
            _context = context;
        }

        public AppReturn Delete(int id)
        {
            try
            {
                Note note = _context.Notes.Find(id);
                note.IsActive = false;
                Log.Info("Successfully deleted note.");

                return AppReturn.Successful("Note deleted successfully.");
            }
            catch (Exception e)
            {
                Log.Error("An unexpected error has occured while deleting note.", e);
                return AppReturn.InvalidOperation("An error has occured when deleting note.");
            }
        }
    }
}
