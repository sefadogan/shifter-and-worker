using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAW.WebApi.Dtos
{
    public class UserRoleDto
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public int StoreId { get; set; }
    }
}