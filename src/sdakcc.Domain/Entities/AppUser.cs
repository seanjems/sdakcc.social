using Abp.Authorization.Users;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace sdakcc.Entities
{
    [NotMapped]
    public class AppUser : AbpUser<AppUser>
    {
        
        public int Relationship { get;  set; }
        public string Address { get;  set; }
        public int Family { get;  set; }
        public int Profession { get;  set; }
        public string Aboutme { get;  set; }
        public int LocalChurch { get;  set; }
        public string Contacts { get;  set; }
        public string FavouriteVerse { get;  set; }

        public List<Follower> Followers { get;  set; }


    }
}
