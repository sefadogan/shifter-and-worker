using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class VMShift
    {
        public int ShiftId { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime CreateDate { get; set; }
        public int DepartmentId { get; set; }
        public int UserId { get; set; }
        public bool IsNotify { get; set; }
        public bool IsActive { get; set; }

        public static VMShift Parse(Shift model)
        {
            VMShift result = new VMShift
            {
                ShiftId = model.ShiftId,
                Date = model.Date,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                CreateDate = model.CreateDate,
                DepartmentId = model.DepartmentId,
                UserId = model.UserId,
                IsNotify = model.IsNotify,
                IsActive = model.IsActive
            };

            return result;
        }
        public static List<VMShift> Parse(List<Shift> models)
        {
            List<VMShift> results = new List<VMShift>();
            foreach (var model in models)
                results.Add(VMShift.Parse(model));

            return results;
        }
    }
}
