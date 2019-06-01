using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class VMUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
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

        public static VMUser Parse(User model)
        {
            VMUser result = new VMUser
            {
                UserId = model.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                About = model.About,
                LastLogin = model.LastLogin,
                CreateDate = model.CreateDate,
                IsActive = model.IsActive,
                RoleId = model.RoleId,
                StoreId = model.StoreId,
                DepartmentId = model.DepartmentId,
                SupervisorId = model.SupervisorId,
                WorkingTypeId = model.WorkingTypeId,
                ImageUrl = model.ImageUrl
            };

            return result;
        }
        public static List<VMUser> Parse(List<User> models)
        {
            List<VMUser> results = new List<VMUser>();
            foreach (var model in models)
                results.Add(VMUser.Parse(model));

            return results;
        }
    }
}
