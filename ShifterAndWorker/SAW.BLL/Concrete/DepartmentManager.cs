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
    public class DepartmentManager : BaseManager<Department, int, ShifterAndWorkerEntities>, IDepartmentManager
    {
        readonly ShifterAndWorkerEntities _context;

        public DepartmentManager(ShifterAndWorkerEntities context) : base(context)
        {
            _context = context;
        }
        public byte BringParentRootValue(int? parentId)
        {
            return _context.Departments.FirstOrDefault(x => x.DepartmentId == parentId).RootLevel;
        }
        public AppReturn Delete(int id)
        {
            try
            {
                Department department = _context.Departments.Find(id);
                department.IsActive = false;
                Log.Info("Successfully deleted department.");

                return AppReturn.Successful("Department deleted successfully.");
            }
            catch (Exception e)
            {
                Log.Error("An unexpected error has occured while deleting department.", e);
                return AppReturn.InvalidOperation("An error has occured when deleting department.");
            }
        }
    }
}
