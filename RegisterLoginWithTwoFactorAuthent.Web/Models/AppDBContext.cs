using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RegisterLoginWithTwoFactorAuthent.Web.Models
{
    public class AppDBContext: IdentityDbContext<AppUser,AppRole,string>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }
    }
}
