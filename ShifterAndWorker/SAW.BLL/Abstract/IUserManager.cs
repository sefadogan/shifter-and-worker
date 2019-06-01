using SAW.BLL.Concrete;
using SAW.Core;
using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SAW.BLL.Abstract
{
    public interface IUserManager : IBaseManager<User, int, ShifterAndWorkerEntities>
    {
        AppReturn Delete(int id);
    }
}
