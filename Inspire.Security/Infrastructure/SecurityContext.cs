
using Microsoft.EntityFrameworkCore;
using Inspire.Security.Models;
namespace Inspire.DataAccess
{
    public partial class SecurityContext: RecordContext
    {
        /// <summary>
        /// Enable configuration of Fluent API in child implementations
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void BuildModels(ModelBuilder modelBuilder)
        {

            base.BuildModels(modelBuilder);
        }
        public DbSet<AuthorLog> AuthorizationLogs { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<ParentMenu> ParentMenus { get; set; }
        public DbSet<SubMenu> SubMenus { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<SystemUserProfile> UserProfiles { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<UserProfileMenu> ProfileMenus { get; set; }
        public DbSet<UserProfileReport> ProfileReports { get; set; }
        public DbSet<RecordAction> RecordActions { get; set; }
        public DbSet<SecurityOption> SecurityOptions { get; set; }
        public DbSet<EmailQueue> EmailQueues { get; set; }
        public DbSet<SysParameter> Parameters { get; set; }
        public DbSet<SystemReportParameter> SystemReportParameters { get; set; }
    }
}
