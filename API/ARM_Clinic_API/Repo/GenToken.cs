using ARM_Clinic_API.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace ARM_Clinic_API_Helper
{
    public class GenToken
    {
        public string GenerateJSONWebToken(int empid, UserProfileResponse userProfile)
        {
            //var claims = new[];
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, empid.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ConnectionStrings.ConfigJWT("Key"));
            var ExpDay = int.Parse(ConnectionStrings.ConfigJWT("ExpiresDay"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", empid.ToString()),
                    new Claim("expdate", DateTime.Now.AddDays(ExpDay).ToString("yyyyMMddHHmmss")),
                    new Claim("FullName",$"{userProfile.Fname}"),
                    new Claim("Email", userProfile.Email),
                }),
                Expires = DateTime.Now.AddDays(ExpDay),
                IssuedAt = DateTime.UtcNow,
                Issuer = ConnectionStrings.ConfigJWT("Issuer"),
                Audience = ConnectionStrings.ConfigJWT("Audience"),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
