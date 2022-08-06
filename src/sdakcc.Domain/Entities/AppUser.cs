using Abp.Authorization.Users;
using Abp.Domain.Entities.Auditing;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Users;

namespace sdakcc.Entities
{
    [NotMapped]
    public class AppUser : AbpUser<AppUser>
    {

        
        public int Relationship { get; protected set; }
        public string Address { get; protected set; }
        public int Family { get; protected set; }
        public int Profession { get; protected set; }
        public string Aboutme { get; protected set; }
        public int LocalChurch { get; protected set; }
        public string Contacts { get; protected set; }
        public string FavouriteVerse { get; protected set; }

        public List<Follower> Followers { get; protected set; }


    }
}
