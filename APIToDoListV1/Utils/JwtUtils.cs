using APIToDoListV1.Entities;
using Microsoft.IdentityModel.Tokens;
using Model.UserModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIToDoListV1.Utils
{
    public class JwtUtils : IJwtUtils
    {
        private readonly IConfiguration _iConfiguration;
        

        public JwtUtils(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
           
        }

        public string GenerateToken(User loginUser)
        {
            var tokenHandel = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_iConfiguration["JwtSecurityKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new[]
                {
                     new Claim(ClaimTypes.Name, loginUser.UserName),
                     new Claim("UserId", loginUser.Id.ToString()),
                     new Claim("Email", loginUser.Email.ToString()),
                     new Claim("FirstName", loginUser.FirstName.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
