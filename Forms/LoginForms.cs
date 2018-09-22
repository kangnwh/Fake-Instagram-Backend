using System.ComponentModel.DataAnnotations;
using System;
using NetCoreApi.Model;

namespace NetCoreApi.Forms
{

    enum GenderEnum {
        Female ,
        Male
    }
    public class LoginForm
    {
        [Required]
        public string Username { set; get; }

        [Required]
        public string Password { set; get; }
    }

    public class SignupForm
    {
        [Required]
        public string Username { set; get; }

        [Required]
        public string Password { set; get; }

        [Required]
        public string Name { set; get; }

        [Required]
        public string Email { set; get; }

        [Required]
        public string Phone { set; get; }

        [Required]
        public DateTime Dob { set; get; }

        [Required]
        [EnumDataType(typeof(GenderEnum))]
        public string Gender { set; get; }

    }
}