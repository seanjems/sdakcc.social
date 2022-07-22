using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace sdakcc.Entities
{
    public class Like : Entity
    {
        public Guid PostId { get; set; }
        public int PostType { get; set; }
        public Guid UserId { get; set; }

        public DateTime CreationTime { get; set; }

        public Like()
        {

        }

        public override object[] GetKeys()
        {
            return new object[] { PostId, UserId };
        }
    }
}
