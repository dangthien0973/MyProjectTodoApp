using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace APIToDoListV1.Entities
{
    public class User : IdentityUser<Guid>
    {
        [MaxLength(250)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(250)]
        [Required]
        public string LastName { get; set; }

    }
}
