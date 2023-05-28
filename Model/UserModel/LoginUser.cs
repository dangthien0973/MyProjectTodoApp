using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserModel
{
    public  class LoginUser
    {
        [Required(ErrorMessage ="UserName must be fill")]
        public string UserName { get; set;}
        [Required(ErrorMessage = "Password must be fill")]
        public string Password { get; set;}
    }
}
