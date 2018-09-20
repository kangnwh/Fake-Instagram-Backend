using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetCoreApi.Model;
using CryptoHelper;
using System.Diagnostics;
using NetCoreApi.Forms;
using System.Collections;

namespace NetCoreApi.Controllers
{
    
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private IConfiguration configuration;
        private snsContext db;


 
        public LoginController(IConfiguration _configuration, snsContext _db)
        {
            configuration = _configuration;
            db = _db;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginForm loginUser ){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            string hashedPw = Crypto.HashPassword(loginUser.Password);
            string username = loginUser.Username;
            Debug.WriteLine($"user:{username}");
            Debug.WriteLine($"user:{hashedPw}");

            User user = db.User.Where( u => u.Username == username && u.Password == hashedPw).FirstOrDefault();

            if(user == null){
                return BadRequest("Invalid username or password");
            }


            // If user is verified, add claims into token
            Claim[] userInfoClaims = new Claim[]{} ;
            userInfoClaims.Append(new Claim(ClaimTypes.Name,user.Username));


            foreach (var r in user.UserRole.ToList())
            {
                    userInfoClaims.Append(new Claim(ClaimTypes.Role,r.Role.RoleName));
            }
                
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Security:Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims:userInfoClaims,
                issuer:configuration["Security:Tokens:Issuer"],
                audience:configuration["Security:Tokens:Audience"],
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials:creds
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(tokenString);
        }

        [HttpPost("sign-up")]
        public IActionResult Signup([FromBody]SignupForm Signup){


            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            Signup.Password = Crypto.HashPassword(Signup.Password);


            User newUser = new User(Signup);
            db.User.Add(newUser);

            db.SaveChanges();

            return Ok("Signup successfully");
        }
        
    }
}