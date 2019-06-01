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
    public class PostManager : BaseManager<Post, int, ShifterAndWorkerEntities>, IPostManager
    {
        public PostManager(ShifterAndWorkerEntities context) : base(context)
        {
        }
    }
}
