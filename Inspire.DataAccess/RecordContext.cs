using Inspire.Modeller.Models.Security;
using Microsoft.EntityFrameworkCore;

namespace Inspire.DataAccess
{
    public partial class RecordContext: DbContext
    {
                
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildModels(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        /// <summary>
        /// Enable configuration of Fluent API in child implementations
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected virtual void BuildModels(ModelBuilder modelBuilder)
        {

        }
    }
}
