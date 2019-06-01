using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.DataModel
{
    public class Post
    {
        public int PostId { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }
}
