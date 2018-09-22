using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreApi.Model
{
    public partial class Post
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
            Image = new HashSet<Image>();
            UserLikePost = new HashSet<UserLikePost>();
        }

        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("userId", TypeName = "int(11)")]
        public int UserId { get; set; }
        [Column("content", TypeName = "varchar(45)")]
        public string Content { get; set; }
        [Column("location", TypeName = "varchar(45)")]
        public string Location { get; set; }
        [Column("createDate", TypeName = "datetime")]
        public DateTime CreateDate { get; set; }
        [Column("logi", TypeName = "decimal(8,3)")]
        public decimal? Logi { get; set; }
        [Column("lati", TypeName = "decimal(8,3)")]
        public decimal? Lati { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Post")]
        public User User { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public ICollection<Image> Image { get; set; }
        [InverseProperty("Post")]
        public ICollection<UserLikePost> UserLikePost { get; set; }
    }
}
