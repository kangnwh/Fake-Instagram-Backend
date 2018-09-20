using System;
using System.Collections.Generic;

namespace NetCoreApi.Model
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserId { get; set; }

        public Post Post { get; set; }
        public User User { get; set; }
    }
}
