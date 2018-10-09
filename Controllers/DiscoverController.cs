using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileBackend.Model;
using MobileBackend.Util;

namespace MobileBackend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DiscoverController: Controller
    {
        private IConfiguration configuration;
        private MobileDbContext db;
        private IHostingEnvironment env;
        public DiscoverController(IConfiguration _configuration, MobileDbContext _db, IHostingEnvironment _env)
        {
            configuration = _configuration;
            db = _db;
            env = _env;
        }

        public IActionResult Index(){

            var userId = User.CurrentUserId();
            if(userId < 0) 
            {
                return BadRequest("User name claim error, cannot find username");
            }
            
            // TODO: calculate suggest friends
            // Algorithm
            var follows = from u in db.User
                                join f in db.FollowRelation
                                on u.Id equals f.From   
                                select f.To;

            var suggest = from u in db.User
                            // join f in db.FollowRelation
                            // on u.Id equals f.From
                            where !follows.Contains(u.Id) && u.Id != userId
                            // group 
                            select new {
                                userId = u.Id,
                                userName = u.Username,
                                avatarUrl = u.AvatarUrl,
                            };

            // var r = from u in db.User
            //         join f in db.FollowRelation
            //         on u.Id equals f.To
            //         where f.From == userId 


            // TODO 
            // Need apply algorithm

            return new JsonResult ( new { users = suggest.ToList()} );
        }
    }
}