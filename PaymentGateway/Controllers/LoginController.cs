using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PaymentGateway.Models;

namespace PaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// If login was successful - returns a new JSON Web Token.
        /// </summary>
        /// <param name="login">User login details</param>
        /// <returns>JSON Web Token / Unauthorized</returns>
        [AllowAnonymous]
        [HttpPost]
        
        public IActionResult Login([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJsonWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJsonWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
                new Claim("MerchantId", userInfo.MerchantId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel AuthenticateUser(LoginModel login)
        {
            // This method would be better suited to being extracted into a proper service - hard coding basic user for now.
            UserModel user = null;

            if (login.Username == "Marcin" && login.Password == "abc123")
            {
                user = new UserModel { Username = "Marcin Brzezinski", EmailAddress = "marcin@brzezinski.net", MerchantId = 1 };
            } else if (login.Username == "Intruder")
            {
                user = new UserModel { Username = "Intruder", EmailAddress = "other@company.com", MerchantId = 100 };
            }
            return user;
        }
    }
}