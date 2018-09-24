using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MobileBackend.Model;
using CryptoHelper;
using System.Diagnostics;
using MobileBackend.Forms;
using System.Collections;
using System.Collections.Generic;

namespace MobileBackend.Controllers
{
    
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private IConfiguration configuration;
        private MobileDbContext db;


 
        public LoginController(IConfiguration _configuration, MobileDbContext _db)
        {
            configuration = _configuration;
            db = _db;
        }

        /// <summary>
        /// Login Interface
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>Succ:Ok(200) or Fail:BadRequest(400)</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginForm loginUser ){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            // string hashedPw = Crypto.HashPassword(loginUser.Password);
            // string username = loginUser.Username;
            // Debug.WriteLine(message: $"user:{username}");
            // Debug.WriteLine(message: $"password:{hashedPw}");

            User user = db.User.Where( u => u.Username == loginUser.Username ).FirstOrDefault();

            if(user == null){
                return BadRequest("Invalid username");
            }

            if (!Crypto.VerifyHashedPassword(user.Password,loginUser.Password)){
                return BadRequest("Invalid password");
            }

            // If user is verified, add claims into token
            List<Claim> userInfoClaims = new List<Claim>();
            // Claim[] userInfoClaims = new Claim[]{} ;
            userInfoClaims.Add(new Claim(ClaimTypes.Name,user.Username));
            userInfoClaims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()));
                
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Security:Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims:userInfoClaims,
                issuer:configuration["Security:Tokens:Issuer"],
                audience:configuration["Security:Tokens:Audience"],
                expires: DateTime.Now.AddDays(1),
                signingCredentials:creds
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(tokenString);
        }

        /// <summary>
        /// Sign Up (Register) interface
        /// </summary>
        /// <param name="signupForm"></param>
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