﻿using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class VMUserCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string About { get; set; }
        public string ImageUrl { get; set; }
        public int? SupervisorId { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public int WorkingTypeId { get; set; }

        public List<User> Supervisors { get; set; }
        public List<Department> Departments { get; set; }
        public List<Role> Roles { get; set; }
        public List<WorkingType> WorkingTypes { get; set; }

        public static VMUserCreate Parse(List<User> supervisors, List<Department> departments, List<Role> roles, List<WorkingType> workingTypes)
        {
            VMUserCreate result = new VMUserCreate
            {
                Supervisors = supervisors,
                Departments = departments,
                Roles = roles,
                WorkingTypes = workingTypes
            };

            return result;
        }
    }
}
