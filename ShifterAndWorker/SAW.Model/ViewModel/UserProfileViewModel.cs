using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAW.Model.ViewModel
{
    public class UserProfileViewModel
    {
        public UserProfileViewModel()
        {
            PostList = new List<Post>();
            User = new User();
        }

        public List<Post> PostList { get; set; }
        public User User { get; set; }
    }
}
