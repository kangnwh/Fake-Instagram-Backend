using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileBackend.Model;
using MobileBackend.Util;
using System.Collections.Generic;

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
//        [HttpPost("likedPhotoList")]
//        public IActionResult LikedPhotoList()
//        {
//            var userId = User.CurrentUserId();
//            List<int> postIdList = new List<int>();
            

//        }
    }
}