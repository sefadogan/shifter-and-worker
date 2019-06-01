using SAW.Core;
using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.BLL.Abstract
{
    public interface IDepartmentManager : IBaseManager<Department, int, ShifterAndWorkerEntities>
    {
        byte BringParentRootValue(int? parentId);
        AppReturn Delete(int id);
    }
}
