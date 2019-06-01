using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class VMAccountSettings
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string About { get; set; }
        public string ImageUrl { get; set; }

        public static VMAccountSettings Parse(User model)
        {
            VMAccountSettings result = new VMAccountSettings
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                Password = model.Password,
                About = model.About,
                ImageUrl = model.ImageUrl
            };

            return result;
        }
    }
}
