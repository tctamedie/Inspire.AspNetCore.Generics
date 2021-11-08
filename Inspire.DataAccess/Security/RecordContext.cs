

namespace Inspire.DataAccess.Security
{
    using Inspire.Modeller.Models.Security;
    using Microsoft.EntityFrameworkCore;
    public partial class SecurityContext: RecordContext
    {
        public SecurityContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ParentMenu> ParentMenus { get; set; }
        public DbSet<SubMenu> SubMenus { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<SystemUserProfile> SystemUserProfiles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserProfileMenu> UserProfileMenus { get; set; }
        public DbSet<UserProfileReport> UserProfileReports { get; set; }
        
    }
}
