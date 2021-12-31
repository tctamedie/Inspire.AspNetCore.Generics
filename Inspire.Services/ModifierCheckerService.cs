using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Services
{
    public interface IModifierCheckerService<TEntity, TMap, T, TDbContext, TFIlter> : IModifierService<TEntity, TMap, T, TDbContext, TFIlter>
        where TEntity : ModifierChecker<T>
        where TMap : ModifierCheckerDto<T>
        where T : IEquatable<T>
        where TDbContext: DbContext
        where TFIlter: RecordStatusFilter
    {
        
    }
    public abstract class  ModifierCheckerService<TEntity, TMap, T, TDbContext, TFilter> : ModifierService<TEntity, TMap, T, TDbContext, TFilter>, 
        IModifierCheckerService<TEntity, TMap, T, TDbContext, TFilter>
        where TEntity : ModifierChecker<T>
        where TMap : ModifierCheckerDto<T>
        where T : IEquatable<T>
        where TDbContext: DbContext
        where TFilter: RecordStatusFilter
    {
        
        public ModifierCheckerService(TDbContext context
            //, IAuditTrailService auditTrailService
            ):base(context)
        {
            
        }
        protected override bool ValidateAuthoriseOnModifier(T id, string user)
        {
            return !Any(s => s.Id.Equals(id) && s.ModifiedBy.ToUpper() == user.ToUpper());
        }
        
        protected override void AppendAuthoriser(TEntity row, string createdBy)
        {
            row.AuthorisedBy = createdBy.ToUpper();
            row.DateAuthorised = DateTime.UtcNow.AddHours(2);
            row.AuthStatus = "A";
        }
        public override IQueryable<TEntity> SearchByFilterModel(TFilter model, IQueryable<TEntity> data = null)
        {
            string status = string.IsNullOrEmpty(model.AuthStatus) ? "U" : model.AuthStatus;
            return _context.Set<TEntity>().Where(s => s.AuthStatus == status);
        }
        public override Task<List<TEntity>> ReadAsync(TFilter model)
        {
            string status = string.IsNullOrEmpty(model.AuthStatus) ? "A" : model.AuthStatus;
            return _context.Set<TEntity>().Where(s => s.AuthStatus == status).ToListAsync();
        }

    }
}
