using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel.Partials
{
    public class VMTopBarPartial
    {
        public User LoggedUser { get; set; }
        public List<Mail> Mails { get; set; }
        public byte NumberOfNewMails { get; set; }
        public List<UserAnnouncement> UserAnnouncements { get; set; }
        public byte NumberOfNewAnnouncements { get; set; }

        public static VMTopBarPartial Synchronize(User user, List<Mail> mails, List<UserAnnouncement> userAnnouncements)
        {
            VMTopBarPartial result = new VMTopBarPartial
            {
                LoggedUser = user,
                Mails = mails,
                NumberOfNewMails = (byte)mails.Where(x => !x.IsRead).Count(),
                UserAnnouncements = userAnnouncements,
                NumberOfNewAnnouncements = (byte)userAnnouncements.Where(x => !x.IsRead).Count()
            };

            return result;
        }


        //public VMTopBarPartial()
        //{
        //    LoggedUser = new VMUser();
        //    Mails = new List<VMMail>();
        //}
        //public VMUser LoggedUser { get; set; }
        //public List<VMMail> Mails { get; set; }
        //public byte NumberOfNewMails { get; set; }
        //public byte NumberOfMails { get; set; }

        //public static VMTopBarPartial Parse(User user, List<Mail> mails)
        //{
        //    VMTopBarPartial result = new VMTopBarPartial();

        //    #region Fills logged user's properties

        //    result.LoggedUser.UserId = user.UserId;
        //    result.LoggedUser.FirstName = user.FirstName;
        //    result.LoggedUser.LastName = user.LastName;

        //    #endregion

        //    #region Fills mail's properties

        //    for (byte i = 0; i < mails.Count(); i++)
        //    {
        //        result.Mails[i].MailId = mails[i].MailId; --> INDEX İÇİN OUT OF RANGE DIYOR ANLAMADIM.
        //        result.Mails[i].Subject = mails[i].Subject;
        //        result.Mails[i].Body = mails[i].Body;
        //        result.Mails[i].SenderFirstName = mails[i].User.FirstName;
        //        result.Mails[i].SenderLastName = mails[i].User.LastName;
        //        result.Mails[i].CreateDate = mails[i].CreateDate;
        //    }

        //    #endregion

        //    result.NumberOfNewMails = (byte)mails.Where(x => !x.IsRead).Count();
        //    result.NumberOfMails = (byte)mails.Count();

        //    return result;
        //}
    }
    //public class VMMail
    //{
    //    public int MailId { get; set; }
    //    public string Subject { get; set; }
    //    public string Body { get; set; }
    //    public string SenderFirstName { get; set; }
    //    public string SenderLastName { get; set; }
    //    public DateTime CreateDate { get; set; }
    //}

    //public class VMUser
    //{
    //    public int UserId { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //}
}
