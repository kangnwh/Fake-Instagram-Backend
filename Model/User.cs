using System;
using System.Collections.Generic;
using NetCoreApi.Forms;
using CryptoHelper;

namespace NetCoreApi.Model
{
    public enum GenderEnum {  
        F ,
        M 
    }  

    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            FollowRelationFromNavigation = new HashSet<FollowRelation>();
            FollowRelationToNavigation = new HashSet<FollowRelation>();
            Post = new HashSet<Post>();
            UserRole = new HashSet<UserRole>();
        }

        public User(SignupForm Signup)
        {
            this.Username = Signup.Username;
            this.Password = Signup.Password;
            this.Dob = Signup.Dob;
            this.Email = Signup.Email;
            this.Phone = Signup.Phone;
            this.Gender = Signup.Gender;
            this.CreateDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public DateTime? CreateDate { get; set; }

        public ICollection<Comment> Comment { get; set; }
        public ICollection<FollowRelation> FollowRelationFromNavigation { get; set; }
        public ICollection<FollowRelation> FollowRelationToNavigation { get; set; }
        public ICollection<Post> Post { get; set; }
        public ICollection<UserRole> UserRole { get; set; }
    }
}
