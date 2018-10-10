using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileBackend.Model;
using MobileBackend.Util;
using System.Collections.Generic;
using System.Linq;

namespace MobileBackend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ActivityFeedController: Controller
    {
        private IConfiguration configuration;
        private MobileDbContext db;
        private IHostingEnvironment env;
        public ActivityFeedController(IConfiguration _configuration, MobileDbContext _db, IHostingEnvironment _env)
        {
            configuration = _configuration;
            db = _db;
            env = _env;
        }

        /// <summary>
        /// get the photo list which the current user like
        /// [Authorization required]
        /// </summary>
        [HttpPost("getLikedPhotoList")]
        public IActionResult GetLikedPhotoList()
        {
            var userId = User.CurrentUserId();
            var request = (
                from like in db.UserLikePost
                join po in db.Post on like.PostId equals po.Id
                join im in db.Image on po.Id equals im.PostId
                join u in db.User on po.UserId equals u.Id
                where like.UserId == userId
                select new
                {
                    postId = po.Id,
                    postUserId = po.UserId,
                    postUserName = u.Username,
                    img = im.ImageUrl,
                    comments = (from com in db.Comment
                                join u01 in db.User on com.UserId equals u01.Id
                                where com.PostId == po.Id
                                select new
                                {
                                    userId = u01.Id,
                                    userName = u01.Username,
                                    content = com.Content,
                                    datetime = com.CreateDate
                                }
                    ).ToList(),
                    datetime = like.CreateDate
                }
                ).ToList();
            return new JsonResult(request);
        }

        /// <summary>
        /// get the user who the current user are following
        /// [Authorization required]
        /// </summary>
        [HttpPost("getFollowingUserList")]
        public IActionResult GetFollowingUserList()
        {
            var userId = User.CurrentUserId();
            var request = (
                    from fo in db.FollowRelation
                    join u in db.User on fo.To equals u.Id
                    where fo.From == userId
                    select new
                    {
                        userId = u.Id,
                        userName = u.Username
                    }
                ).ToList();
            return new JsonResult(request);
        }



    }
}