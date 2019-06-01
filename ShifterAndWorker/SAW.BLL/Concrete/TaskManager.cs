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
    public class TaskManager : BaseManager<DAL.Context.Task, int, ShifterAndWorkerEntities>, ITaskManager
    {
        public TaskManager(ShifterAndWorkerEntities context) : base(context)
        {
        }
    }
}
