using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileBackend.Model;
using MobileBackend.Util;

using System;

namespace MobileBackend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserFeedController: Controller
    {
        private IConfiguration configuration;
        private MobileDbContext db;
        private IHostingEnvironment env;
         public UserFeedController(IConfiguration _configuration, MobileDbContext _db, IHostingEnvironment _env)
        {
            configuration = _configuration;
            db = _db;
            env = _env;
        }

        /// <summary>
        /// for Scroll through photos and comments
        /// [Authorization required]
        /// </summary>
        //public iactionresult refreshcontent()
        //{
        //    var userid = user.currentuserid();
        //    // question: whose posts will be return?
        //}

        /// <summary>
        /// set "like" to current post
        /// [Authorization required]
        /// </summary>
        [HttpPost("likePhoto")]
        public IActionResult LikePhoto(int postId)
        {
            var userId = User.CurrentUserId();
            if (postId < 0)
                return BadRequest("User name claim error, cannot find PostID");

            UserLikePost like = new UserLikePost();
            like.UserId = userId;
            like.PostId = postId;
            Post post = db.Post.Where(p => p.Id == postId).FirstOrDefault();
            if (post == null)
                return BadRequest("User name claim error, cannot find PostID");

            like.PostUserId = post.UserId;
            like.CreateDate = DateTime.Now;
            db.UserLikePost.Add(like);
            db.SaveChanges();
            return Ok("like successfully");
        }

        /// <summary>
        /// del "like" from UserLikePost table
        /// [Authorization required]
        /// </summary>
        [HttpPost("dislikePhoto")]
        public IActionResult DislikePhoto(int postId)
        {
            var userId = User.CurrentUserId();
            if (postId < 0)
                return BadRequest("User name claim error, cannot find PostID");

            UserLikePost like = new UserLikePost();
            like.UserId = userId;
            like.PostId = postId;
            Post post = db.Post.Where(p => p.Id == postId).FirstOrDefault();
            if (post == null)
                return BadRequest("User name claim error, cannot find PostID");

            like.PostUserId = post.UserId;
            like.CreateDate = DateTime.Now;
            db.UserLikePost.Remove(like);
            db.SaveChanges();
            return Ok("Cancel Like successfully");
        }

        /// <summary>
        /// create new post comment
        /// [Authorization required]
        /// </summary>
        [HttpPost("comment")]
        public IActionResult Comment(int postId, string commentContent)
        {
            var userId = User.CurrentUserId();
            if (postId < 0)
                return BadRequest("User name claim error, cannot find PostID");
            if (db.Post.Find(postId) == null)
                return BadRequest("User name claim error, cannot find PostID");

            Comment comment = new Comment();
            comment.PostId = postId;
            comment.UserId = userId;
            if (String.IsNullOrEmpty(commentContent))
                commentContent = "";
            if (commentContent.Length < 100)
                comment.Content = commentContent;
            if (commentContent.Length >= 100)
                return BadRequest("Your comment is too long. Comment's content is limited within 100 characters.");
            comment.CreateDate = DateTime.Now;
            db.Comment.Add(comment);
            db.SaveChanges();
            return Ok("Leave comment successfully");
        }
    }
}