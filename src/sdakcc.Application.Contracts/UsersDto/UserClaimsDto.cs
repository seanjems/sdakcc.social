using System;
using System.Collections.Generic;
using System.Text;

namespace sdakcc.UsersDto
{
    public class UserClaimsDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public long UserId { get; set; }
        public long? TenantId { get; set; }

    }
}
