﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UserModel
{
    public  class LoginReponse
    {
        public string Token { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }
}
