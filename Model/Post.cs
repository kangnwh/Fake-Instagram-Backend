using System;
using System.Collections.Generic;

namespace NetCoreApi.Model
{
    public partial class Post
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
            Image = new HashSet<Image>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }

        public User User { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public ICollection<Image> Image { get; set; }
    }
}
