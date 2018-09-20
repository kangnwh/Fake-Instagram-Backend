using System;
using System.Collections.Generic;

namespace NetCoreApi.Model
{
    public partial class Image
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? CreateDate { get; set; }

        public Post Post { get; set; }
    }
}
