using SAW.Core;
using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SAW.BLL.Abstract
{
    public interface IMailManager : IBaseManager<Mail, int, ShifterAndWorkerEntities>
    {
        AppReturn Delete(int id);
        AppReturn PermanentDelete(int id);
        Mail BringById(Expression<Func<Mail, bool>> filter);
    }
}
