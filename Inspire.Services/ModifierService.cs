using Inspire.Annotator;
using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Services
{

    public interface IModifierService<TEntity, TMap, T,TDbContext, TFilter> : IMakerService<TEntity, TMap, T, TDbContext, TFilter>
        where TEntity : Modifier<T>
        where TMap : ModifierDto<T>
        where T : IEquatable<T>
        where TDbContext: DbContext
        where TFilter: RecordFilter
    {
        
    }
    public abstract class  ModifierService<TEntity, TMap, T,TDbContext, TFilter> : MakerService<TEntity, TMap, T, TDbContext, TFilter>, IModifierService<TEntity, TMap, T, TDbContext, TFilter>
        where TEntity : Modifier<T>, new()
        where TMap : ModifierDto<T>
        where T : IEquatable<T>
        where TDbContext: DbContext
        where TFilter: RecordFilter
    {
        
        public ModifierService(TDbContext context
            //, IAuditTrailService auditTrailService
            ):base(context)
        {
            
        }
        protected override bool ValidateDeleteOnModifier(T id, string user)
        {
            return !Any(s=>s.Id.Equals(id)&&s.ModifiedBy.ToUpper()==user.ToUpper());
        }
        protected override void AppendModifier(TEntity row, string updatedBy)
        {
            row.ModifiedBy = updatedBy.ToUpper();
            row.DateModified = DateTime.UtcNow.AddHours(2);
        }

    }
}
