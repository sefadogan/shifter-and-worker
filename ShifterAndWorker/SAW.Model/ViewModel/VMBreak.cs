using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class VMBreak
    {
        public int BreakId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }

        public static VMBreak Parse(Break model)
        {
            VMBreak result = new VMBreak
            {
                BreakId = model.BreakId,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                CreateDate = model.CreateDate,
                IsActive = model.IsActive
            };

            return result;
        }
        public static List<VMBreak> Parse(List<Break> models)
        {
            List<VMBreak> results = new List<VMBreak>();
            foreach (var model in models)
                results.Add(VMBreak.Parse(model));

            return results;
        }
    }
}
