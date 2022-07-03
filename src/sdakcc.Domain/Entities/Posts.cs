using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace sdakcc.Entities
{
    public class Posts : AuditedEntity<Guid>
    {
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public int PostType { get; set; }
        public string Image { get; set; }
        public List<Like> Likes { get; set; }
    }
}
