using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileBackend.Model;
using MobileBackend.Forms;
using MobileBackend.Util;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MobileBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private IConfiguration configuration;
        private MobileDbContext db;
        private IHostingEnvironment env;
        public UploadController(IConfiguration _configuration, MobileDbContext _db, IHostingEnvironment _env)
        {
            configuration = _configuration;
            db = _db;
            env = _env;
        }

        [HttpPost("upload")]

        public async System.Threading.Tasks.Task<IActionResult> UploadAsync(IFormFile file, CreatePost post)
        {
            var uploads = Path.Combine(env.WebRootPath, "photos");
            if (file.Length > 0)
            {
                var userId = User.CurrentUserId();
                var FileName = $"{userId}{DateTime.Now.ToString("yyyy_MM_ddTHH_mm_ss")}";
                using (var fileStream = new FileStream(Path.Combine(uploads, FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    var createDate = DateTime.Now;
                    var newPost = new Post
                    {
                        UserId = userId,
                        Content = post.Content,
                        Lati = post.Lati,
                        Logi = post.Logi,
                        Location = post.Location,
                        CreateDate = createDate
                    };

                    var image = new Image
                    {
                        Post = newPost,
                        UserId = userId,
                        CreateDate = createDate,
                        ImageUrl = FileName
                    };
                    try
                    {
                        db.Post.Add(newPost);
                        db.Image.Add(image);
                        db.SaveChanges();
                    }
                    catch (DbUpdateException e)
                    {
                        //This either returns a error string, or null if it can’t handle that error

                        Debug.WriteLine(e.ToString());
                        return BadRequest("upload failed"); //return the error string

                        // throw; //couldn’t handle that error, so rethrow
                    }
                    return Ok("Upload successfully");
                }
            }
            return BadRequest("Upload failed");
        }


    }
}