using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAW.WebApi.Dtos
{
    public class UserForListDto
    {
        //public UserForListDto()
        //{
        //    Mails = new HashSet<Mail>();
        //    Messages = new HashSet<Message>();
        //    Posts = new HashSet<Post>();
        //    Tasks = new HashSet<Task>();
        //    UserAnnouncements = new HashSet<UserAnnouncement>();
        //    UserNotifications = new HashSet<UserNotification>();
        //    UserShifts = new HashSet<UserShift>();
        //}

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

        //public Department Department { get; set; }
        //public ICollection<Mail> Mails { get; set; }
        //public ICollection<Message> Messages { get; set; }
        //public ICollection<Post> Posts { get; set; }
        //public Role Role { get; set; }
        //public Store Store { get; set; }
        //public ICollection<Task> Tasks { get; set; }
        //public ICollection<UserAnnouncement> UserAnnouncements { get; set; }
        //public ICollection<UserNotification> UserNotifications { get; set; }
        //public WorkingType WorkingType { get; set; }
        //public ICollection<UserShift> UserShifts { get; set; }
    }
}