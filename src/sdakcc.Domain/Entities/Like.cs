using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace sdakcc.Entities
{
    public class Like : AuditedEntity<Guid>
    {
        public Guid PostId { get; set; }
        public int PostType { get; set; }
        public Guid UserId { get; set; }
    }
}
