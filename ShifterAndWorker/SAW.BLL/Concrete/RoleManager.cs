using SAW.BLL.Abstract;
using SAW.Core;
using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.BLL.Concrete
{
    class RoleManager : BaseManager<Role, int, ShifterAndWorkerEntities> , IRoleManager
    {
        public RoleManager(ShifterAndWorkerEntities context) : base(context)
        {
        }
    }
}
