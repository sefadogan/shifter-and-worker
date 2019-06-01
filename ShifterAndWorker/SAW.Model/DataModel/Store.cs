using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.DataModel
{
    public class Store
    {
        public int StoreId { get; set; }
        public string StoreCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte? MaxNumberEmployees { get; set; }
        public byte? MinNumberEmployees { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public int AddressId { get; set; }
        public int CompanyId { get; set; }
    }
}
