using Inspire.Annotator;
using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Services.Infrastructure.Common
{

    
    public abstract class ModifierService<TEntity, TMap, T, TDbContext, TFilter>(TDbContext context
            //, IAuditTrailService auditTrailService
            ) : MakerService<TEntity, TMap, T, TDbContext, TFilter>(context), Application.Common.IModifierService<TEntity, TMap, T, TFilter>
        where TEntity : Modifier<T>, new()
        where TMap : ModifierDto<T>
        where T : IEquatable<T>
        where TDbContext : DbContext
        where TFilter : RecordFilter
    {
        protected override bool ValidateDeleteOnModifier(T id, string user)
        {
            return !Any(s => s.Id.Equals(id) && s.ModifiedBy.ToUpper() == user.ToUpper());
        }
        protected override void AppendModifier(TEntity row, string updatedBy)
        {
            row.ModifiedBy = updatedBy.ToUpper();
            row.DateModified = DateTime.UtcNow.AddHours(2);
        }

    }
}
