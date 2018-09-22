using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreApi.Model
{
    [Table("comment")]
    public partial class Comment
    {
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("postId", TypeName = "int(11)")]
        public int PostId { get; set; }
        [Column("userId", TypeName = "int(11)")]
        public int UserId { get; set; }
        [Column("content", TypeName = "varchar(100)")]
        public string Content { get; set; }
        [Column("createDate", TypeName = "datetime")]
        public DateTime CreateDate { get; set; }

        public Post Post { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Comment")]
        public User User { get; set; }
    }
}
