using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace sdakcc.UsersDto
{
    public class CreateUserDto
    {
        //[Required]        
        //public string AppName { get; set; }
        [Required]
        [MinLength(4)]
        public string Password { get; set; }
        [Required, EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required, MinLength(3), MaxLength(15)]
        public string Name { get; set; }
        [Required, MinLength(3), MaxLength(15)]
        public string Surname { get; set; }


        //public string Firstname { get; set; }
        //public string Lastname { get; set; }
        //public int Relationship { get; set; }
        //public string Address { get; set; }
        //public int Family { get; set; }
        //public int Profession { get; set; }
        //public string Aboutme { get; set; }
        //public int LocalChurch { get; set; }
        //public string Contacts { get; set; }
        //public string FavouriteVerse { get; set; }


    }
}
