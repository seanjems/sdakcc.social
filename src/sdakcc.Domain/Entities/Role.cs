using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace sdakcc.Entities
{
    public class Role : IdentityRole
    {
        public const int MaxDescriptionLength = 5000;

        

        [StringLength(MaxDescriptionLength)]
        public string Description { get; set; }
    }
}