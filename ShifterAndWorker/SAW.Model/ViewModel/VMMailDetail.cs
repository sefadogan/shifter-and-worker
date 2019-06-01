using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class VMMailDetail
    {
        public VMMailDetail()
        {
            ChildMails = new List<VMMailDetail>();
        }

        public int MailId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderLastName{ get; set; }

        public static VMMailDetail Parse(Mail mail, User user)
        {
            VMMailDetail result = new VMMailDetail
            {
                MailId = mail.MailId,
                Subject = mail.Subject,
                Body = mail.Body,
                CreateDate = mail.CreateDate,
                SenderId = mail.SenderId,
                ReceiverId = mail.ReceiverId,

                SenderFirstName = user.FirstName,
                SenderLastName = user.LastName
            };

            return result;
        }

        public List<VMMailDetail> ChildMails { get; set; }
    }
}
