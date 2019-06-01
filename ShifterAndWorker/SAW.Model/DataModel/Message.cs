using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.DataModel
{
    public class Message
    {
        public int MessageId { get; set; }
        public int? ParentMessageId { get; set; }
        public bool IsActive { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ReadDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
