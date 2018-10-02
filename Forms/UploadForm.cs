using System.ComponentModel.DataAnnotations;
using System;
using MobileBackend.Model;

namespace MobileBackend.Forms
{
    public class CreatePost
    {
        [Required]
        public string Content { set; get; }

        [Required]
        public string Location { set; get; }

        public decimal? Logi { set; get; }

        public decimal? Lati { set; get; }
    }

   
}