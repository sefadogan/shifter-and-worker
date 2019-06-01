﻿using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.BLL.Abstract
{
    public interface IShiftBreakManager : IBaseManager<ShiftBreak, int, ShifterAndWorkerEntities>
    {
    }
}
