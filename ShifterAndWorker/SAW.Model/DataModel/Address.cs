using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.DataModel
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string AddressType { get; set; }
        public int ZipCode { get; set; }
        public int ProvinceId { get; set; }
        public int TownId { get; set; }
        public int RegionId { get; set; }
    }
}
