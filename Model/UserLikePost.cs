using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreApi.Model
{
    public partial class UserLikePost
    {
        [Column("userId", TypeName = "int(11)")]
        public int UserId { get; set; }
        [Column("postId", TypeName = "int(11)")]
        public int PostId { get; set; }
        [Column("Post_userId", TypeName = "int(11)")]
        public int PostUserId { get; set; }
        [Column("createDate", TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }

        [ForeignKey("PostId,PostUserId")]
        [InverseProperty("UserLikePost")]
        public Post Post { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("UserLikePost")]
        public User User { get; set; }
    }
}
