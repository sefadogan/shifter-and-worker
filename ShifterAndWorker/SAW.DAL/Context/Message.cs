//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SAW.DAL.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class Message
    {
        public int MessageId { get; set; }
        public Nullable<int> ParentMessageId { get; set; }
        public string Message1 { get; set; }
        public bool IsActive { get; set; }
        public bool IsRead { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ReadDate { get; set; }
        public System.DateTime DeleteDate { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    
        public virtual User User { get; set; }
    }
}
