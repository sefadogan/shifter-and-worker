using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAW.WebApi.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string About { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public int StoreId { get; set; }
        public int DepartmentId { get; set; }
        public int? SupervisorId { get; set; }
        public int WorkingTypeId { get; set; }
        public string ImageUrl { get; set; }
    }
}