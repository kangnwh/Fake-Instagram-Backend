using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MobileBackend.Forms;

namespace MobileBackend.Model
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            FollowRelationFromNavigation = new HashSet<FollowRelation>();
            FollowRelationToNavigation = new HashSet<FollowRelation>();
            Image = new HashSet<Image>();
            Post = new HashSet<Post>();
            UserLikePost = new HashSet<UserLikePost>();
        }

        
        public User(SignupForm signupForm)
        {
            this.Username = signupForm.Username;
            this.Password = signupForm.Password;
            this.Name = signupForm.Name;
            this.Email = signupForm.Email;
            this.Phone = signupForm.Phone;
            this.Dob = signupForm.Dob;
            this.Gender = signupForm.Gender;
        }

        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column("username", TypeName = "varchar(45)")]
        public string Username { get; set; }
        [Required]
        [Column("password", TypeName = "varchar(45)")]
        public string Password { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(45)")]
        public string Name { get; set; }
        [Column("email", TypeName = "varchar(45)")]
        public string Email { get; set; }
        [Column("phone", TypeName = "varchar(45)")]
        public string Phone { get; set; }
        [Column("dob", TypeName = "datetime")]
        public DateTime? Dob { get; set; }
        [Column("gender", TypeName = "char(1)")]
        public string Gender { get; set; }
        [Column("createDate", TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        [Column("avatarURL", TypeName = "varchar(45)")]
        public string AvatarUrl { get; set; }

        [InverseProperty("User")]
        public ICollection<Comment> Comment { get; set; }
        [InverseProperty("FromNavigation")]
        public ICollection<FollowRelation> FollowRelationFromNavigation { get; set; }
        [InverseProperty("ToNavigation")]
        public ICollection<FollowRelation> FollowRelationToNavigation { get; set; }
        [InverseProperty("User")]
        public ICollection<Image> Image { get; set; }
        [InverseProperty("User")]
        public ICollection<Post> Post { get; set; }
        [InverseProperty("User")]
        public ICollection<UserLikePost> UserLikePost { get; set; }
    }



}
