using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Inspire.Services
{
    public interface IStandardModifierService<TEntity, TMap, T, TDbContext, TFilter> : IModifierService<TEntity, TMap, T, TDbContext, TFilter>
        where TEntity : StandardModifier<T>
        where TMap : StandardModifierDto<T>
        where T : IEquatable<T>
        where TDbContext: DbContext
        where TFilter: StandardFilter
    {

    }
    public class StandardModifierService<TEntity, TMap, T, TDbContext, TFilter> : ModifierService<TEntity, TMap, T, TDbContext, TFilter>, IStandardModifierService<TEntity, TMap, T, TDbContext, TFilter>
        where TEntity : StandardModifier<T>
        where TMap : StandardModifierDto<T>
        where T : IEquatable<T>
        where TDbContext: DbContext
        where TFilter: StandardFilter
    {
        
        public StandardModifierService(TDbContext context
            //, IAuditTrailService auditTrailService
            ) : base(context)
        {
            
            //_auditTrailService = auditTrailService;
        }
        public override IQueryable<TEntity> SearchByFilterModel(TFilter model, IQueryable<TEntity> data = null)
        {
            string name = string.IsNullOrEmpty(model.Name) ? "" : model.Name.ToLower();
            return _context.Set<TEntity>().Where(s => s.Name.ToLower().Contains(name));
        }
        protected override OutputModel Validate(TMap row, [CallerMemberName] string caller = "")
        {
            var validation = base.Validate(row, caller);
            if (validation.Error)
                return validation;

            if (Any(s => s.Name.ToUpper() == row.Name.ToUpper() && !s.Id.Equals(row.Id)))
            {
                return new OutputModel(true)
                {
                    
                    Message = $" Name {row.Name}for {_modelHeader} already exist"
                };
            }
            return new OutputModel();

        }

        
    }
}
