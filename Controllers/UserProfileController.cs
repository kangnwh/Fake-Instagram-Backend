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
    public class UserProfileController:Controller
    {
        private IConfiguration configuration;
        private MobileDbContext db;

        public UserProfileController(IConfiguration _configuration, MobileDbContext _db)
        {
            configuration = _configuration;
            db = _db;
        }

        /// <summary>
        /// get current user's post statistics.
        /// [Authorization required]
        /// </summary>
        /// <returns></returns>
        [HttpPost("poststat")]
        public IActionResult PostStat(int id = -1)
        {   
            // Use this to get username
            // var userName = User.CurrentUserName();
            var userId = User.CurrentUserId();
            if(id > 0) 
            {
                userId = id;
            }
            
            var username = db.User.Find(userId).Username;
            var postCount = db.Post.Where(p => p.UserId == userId).Count();
            var followerCount = db.FollowRelation.Where( f => f.To == userId).Count();
            var followingCount = db.FollowRelation.Where(f => f.From == userId).Count();
            var avatar = db.User.Find(userId).AvatarUrl;

            var resultDict = new Dictionary<string, object>();
            resultDict.Add("username",username);
            resultDict.Add("postCount",postCount);
            resultDict.Add("followerCount",followerCount);
            resultDict.Add("followingCount",followingCount);
            resultDict.Add("avatar",avatar);
            return new JsonResult ( resultDict );

        }

        /// <summary>
        /// get all current users photos
        /// [Authorization required]
        /// </summary>
        /// <returns></returns>
        [HttpPost("myphotos")]
        public IActionResult MyPhotos(int id = -1)
        {   
            var userId = User.CurrentUserId();
            if(id > 0){
                userId = id ;
            }
            
            if(userId < 0) 
            {
                return BadRequest("User name claim error, cannot find username");
            }
            
            var pJson = (from p in db.Post
                            join i in db.Image on p.Id equals i.PostId
                            where p.UserId == userId 
                        select new {
                            postUserId = p.UserId,
                            postId = p.Id,
                            img = i.ImageUrl,
                            postContent = p.Content,
                            postLocation = p.Location,
                            postTime = p.CreateDate,
                            comments = p.Comment
                        }).ToList();
            return new JsonResult ( pJson );

        }

        [AllowAnonymous]
        [HttpPost("test")]
        public IActionResult test()
        {   
            
            List<Dictionary<string,string>> imgList = new List<Dictionary<string, string>>();

            var resultDict = new Dictionary<string, string>();
            resultDict.Add("img",$"{this.configuration.PhotoFolder()}/test.jpg");

            var resultDict2 = new Dictionary<string, string>();
            resultDict2.Add("img",$"{this.configuration.PhotoFolder()}/test.jpg");


            imgList.Add(resultDict);
            imgList.Add(resultDict2);
            
            return new JsonResult ( imgList );

        }
        
    }
}