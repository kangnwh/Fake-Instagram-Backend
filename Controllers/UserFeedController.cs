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
using MobileBackend.Forms;

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
        /// set "like" to current post
        /// [Authorization required]
        /// </summary>
        [HttpPost("likePhoto")]
        public IActionResult LikePhoto(int postId)
        {
            var userId = User.CurrentUserId();
            if (postId < 0)
                return BadRequest("PostId claim error, cannot find PostID");

            UserLikePost like = new UserLikePost();
            like.UserId = userId;
            like.PostId = postId;
            Post post = db.Post.Where(p => p.Id == postId).FirstOrDefault();
            if (post == null)
                return BadRequest("PostId claim error, cannot find PostID");

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
        [HttpDelete("dislikePhoto")]
        public IActionResult DislikePhoto(int postId)
        {
            var userId = User.CurrentUserId();
            if (postId < 0)
                return BadRequest("PostId claim error, cannot find PostID");

            UserLikePost like = new UserLikePost();
            like.UserId = userId;
            like.PostId = postId;
            Post post = db.Post.Where(p => p.Id == postId).FirstOrDefault();
            if (post == null)
                return BadRequest("PostId claim error, cannot find PostID");

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
        public IActionResult Comment([FromBody] CommentForm commentForm)
        {
            var userId = User.CurrentUserId();
            if (commentForm.postId < 0)
                return BadRequest("PostId invalid, cannot find PostID");
            
            var p = db.Post.Where( r => r.Id == commentForm.postId).FirstOrDefault();
            if (p == null)
                return BadRequest("PostId invalid, cannot find PostID");

            Comment comment = new Comment();
            comment.PostId = commentForm.postId;
            comment.UserId = userId;
            var commentContent = commentForm.commentContent;

            if (String.IsNullOrEmpty(commentContent))
                comment.Content = "";
                
            if (commentContent.Length < 100)
                comment.Content = commentForm.commentContent;

            if (commentContent.Length >= 100)
                return BadRequest("Your comment is too long. Comment's content is limited within 100 characters.");

            comment.CreateDate = DateTime.Now;
            db.Comment.Add(comment);
            db.SaveChanges();
            return Ok("Leave comment successfully");
        }

        [HttpPost("refresh")]
        public IActionResult refresh(int userId = -1)
        {
            if (userId == -1)
                userId = User.CurrentUserId();
            else if (db.User.Find(userId) == null)
                return BadRequest("UserId claim error, cannot find UserId");

            var r1 = (from u in db.User
                    join fo in db.FollowRelation on u.Id equals fo.To
                    join po in db.Post on u.Id equals po.UserId
                    join image in db.Image on po.Id equals image.PostId
                     where fo.From == userId // || po.UserId == u.Id
                    orderby po.CreateDate descending
                    select new {
                        postUserId = u.Id,
                        postUsername = u.Name,
                        postUserAvatarURL = u.AvatarUrl,
                        postContent = po.Content,
                        postId = po.Id,
                        img = image.ImageUrl,
                        postTime = po.CreateDate,
                        postLocation = po.Location,
                    }
                    ).Take(10).ToList();

            var r2 = (from post in r1
                    select new {
                        postuserId = post.postUserId,
                        postUsername = post.postUsername,
                        avatarImageURL = post.postUserAvatarURL,
                        postId = post.postId,
                        postContent = post.postContent,
                        img = post.img,
                        postTime = post.postTime,
                        postLocation = post.postLocation,
                        likeCount = db.UserLikePost.Where( ul => ul.PostId == post.postId).Count(),
                        likeUserList = (from ul01 in db.UserLikePost 
                                        join u01 in db.User on ul01.UserId equals u01.Id
                                        where post.postId == ul01.PostId
                                        select u01.Username).ToList(),
                        comments = (from com01 in db.Comment 
                                    join u02 in db.User on com01.UserId equals u02.Id
                                    where com01.PostId == post.postId
                                    select new {
                                        userid = u02.Id,
                                        username = u02.Username,
                                        content = com01.Content,
                                        datetime = com01.CreateDate
                                    }).ToList()

                    }
            ).ToList();

            return new JsonResult ( r2 );
        }
    }
}