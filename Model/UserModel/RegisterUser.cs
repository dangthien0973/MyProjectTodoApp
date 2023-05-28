using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserModel
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "Your FirstName can not blank please fill it")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Your LastName can not blank please fill it")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Your user must not null !!!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Your Password is not null !!!")]
        public string Password { get; set; }
      public string Email { get; set; }
      public string PhoneNumbers { get; set; }
    }
}
