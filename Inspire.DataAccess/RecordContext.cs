using Inspire.Modeller.Models.Security;
using Microsoft.EntityFrameworkCore;

namespace Inspire.DataAccess
{
    public partial class RecordContext: DbContext
    {
        public RecordContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<AuthorLog> AuthorLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditLog>().HasKey(s => new { s.RecordKey, s.Username, s.AuditDate, s.Table, s.Action });
            modelBuilder.Entity<AuthorLog>().HasKey(s => new { s.RecordKey, s.Username, s.AuthorDate, s.Table, s.AuthorCount });
            base.OnModelCreating(modelBuilder);
        }
    }
}
