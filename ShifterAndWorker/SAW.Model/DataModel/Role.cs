﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.DataModel
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public int StoreId { get; set; }
    }
}
