using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileBackend.Model
{
    public partial class Image
    {


        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("postId", TypeName = "int(11)")]
        public int PostId { get; set; }
        [Column("userId", TypeName = "int(11)")]
        public int UserId { get; set; }
        [Column("imageURL", TypeName = "varchar(45)")]
        public string ImageUrl { set; get;}
        [Column("createDate", TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }

        public Post Post { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Image")]
        public User User { get; set; }
    }
}
