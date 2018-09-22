



using Microsoft.EntityFrameworkCore;

namespace NetCoreApi.Model
{
    public class MobileDbContext : DbContext
    {
        public MobileDbContext(DbContextOptions<MobileDbContext> options)
            : base(options)
        {

        }

    }

}