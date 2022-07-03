using System;
using System.Collections.Generic;
using System.Text;

namespace sdakcc.UsersDto
{
    public class GetAllUsersDto
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Relationship { get; set; }
        public string Address { get; set; }
        public int Family { get; set; }
        public int Profession { get; set; }
        public string Aboutme { get; set; }
        public int LocalChurch { get; set; }
        public string Contacts { get; set; }
        public string FavouriteVerse { get; set; }

        public IEnumerable<FollowersList> Followers { get; set; }
    }
}
