using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace MobileBackend.Util
{
    public static class MyExtensions
    {

        public static string PhotoFolder(this IConfiguration configuration)
        {
            return configuration["DATA_FOLDER:PHOTO"];
        }

        public static int CurrentUserId(this ClaimsPrincipal user )
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if(userIdClaim == null) //null
            {
                return -1;
            }
            
            var userId = int.Parse(userIdClaim.Value);
            return userId;
        }

        public static string CurrentUserName(this ClaimsPrincipal user )
        {
            var userIdClaim = user.FindFirst(ClaimTypes.Name);
            if(userIdClaim == null) //null
            {
                return null;
            }
            
            return userIdClaim.Value;
        }

    }
}