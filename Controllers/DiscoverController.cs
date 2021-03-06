using System;
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
        [HttpPost("index")]
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
                                where u.Id == userId
                                select f.To;

            var suggest = from u in db.User
                            // join f in db.FollowRelation
                            // on u.Id equals f.From
                            where !follows.Contains(u.Id) && u.Id != userId
                            // group 
                            select new {
                                userId = u.Id,
                                userName = u.Username,
                                nickName = u.Name,
                                avator = u.AvatarUrl
                                // followedByCurrentUser = null
                            };

            // TODO 
            // Need apply algorithm

            return new JsonResult ( suggest.ToList() );
        }


        /// <summary>
        /// let user to follow other user
        /// </summary>
        [HttpGet("followUser")]
        public IActionResult FollowUser(int userId)
        {
            var myUserId = User.CurrentUserId();
            if (db.User.Find(userId) == null)
                return BadRequest($"Cannot find UserId {userId}");

            
            // FollowRelation follow = new FollowRelation();
            var follow = db.FollowRelation.Where( r => r.From == myUserId && r.To == userId).FirstOrDefault();
            if(follow != null){
                return BadRequest("User already follows this user.");
            }
            follow = new FollowRelation();
            follow.From = myUserId;
            follow.To = userId;
            follow.CreateDate = DateTime.Now;
            db.FollowRelation.Add(follow);
            db.SaveChanges();
            return Ok("Following user successfully");
        }

        /// <summary>
        /// let user to cancel the follow relationship with other user
        /// </summary>
        [HttpGet("cancelFollowUser")]
        public IActionResult CancelFollowUser(int userId)
        {
            var myUserId = User.CurrentUserId();
            if (db.User.Find(userId) == null)
                return BadRequest("UserId claim error, cannot find UserId");

            // FollowRelation follow = new FollowRelation();
            var follow = db.FollowRelation.Where( r => r.From == myUserId && r.To == userId).FirstOrDefault();
            // follow.From = myUserId;
            // follow.To = userId;
            if(follow == null){
                return BadRequest("User does not follow this user");
            }
            
            db.FollowRelation.Remove(follow);
            db.SaveChanges();

            return Ok("Cancel the Follow relationship successfully");
        }

        /// <summary>
        /// let user to cancel the follow relationship with other user
        /// </summary>
        [HttpGet("queryUser")]
        public IActionResult QueryUser(string username)
        {
            var currentUserId = User.CurrentUserId();

            var currentUserFollowing = from fo in db.FollowRelation
                    join u in db.User on fo.To equals u.Id
                    where fo.From == currentUserId
                    select new
                    {
                        currentFollowing = fo.To
                    };


            var queryUser = from u in db.User
                            where u.Username.Contains(username, StringComparison.OrdinalIgnoreCase)
                            select new {
                                 userId = u.Id,
                                    userName = u.Username,
                                    nickName = u.Name,
                                    avator = u.AvatarUrl,
                                    followedByCurrentUser = (from cu in currentUserFollowing
                                                                where cu.currentFollowing == u.Id 
                                                                select  1 ).FirstOrDefault()
                            };
            
            // db.User.Where( u => u.Username.Contains(username, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            // if (queryUser == null)
            //     return Ok("User not found");

            
            return new JsonResult (queryUser);
        }


    }
}