using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Services.Infrastructure.Common
{
    
    public abstract class ModifierCheckerService<TEntity, TMap, T, TDbContext, TFilter> (TDbContext _context) : ModifierService<TEntity, TMap, T, TDbContext, TFilter>(_context),
        Application.Common.IModifierCheckerService<TEntity, TMap, T, TFilter>
        where TEntity : ModifierChecker<T>, new()
        where TMap : ModifierCheckerDto<T>
        where T : IEquatable<T>
        where TDbContext : DbContext
        where TFilter : RecordStatusFilter
    {

        
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
