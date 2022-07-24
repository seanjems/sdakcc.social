using IdentityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace sdakcc.Entities
{
    public class Like : Entity
    {
        public Guid PostId { get; set; }
        public int PostType { get; set; }
        public Guid UserId { get; set; }

        public DateTime CreationTime { get; set; }
               
        [ForeignKey("UserId")]
        public virtual IdentityUser Users { get; set; }
        public Like()
        {

        }

        public override object[] GetKeys()
        {
            return new object[] { PostId, UserId };
        }
    }
}
