using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MobileBackend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ActivityFeedController: Controller
    {
        
    }
}