using System.ComponentModel.DataAnnotations;
using System;
using MobileBackend.Model;




namespace MobileBackend.Forms
{
    public class CommentForm
    {
        [Required]
        public string commentContent { set; get; }

        [Required]
        public int postId { set; get; }

    }
}
