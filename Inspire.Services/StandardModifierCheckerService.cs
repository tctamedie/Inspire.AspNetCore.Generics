using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Inspire.Services
{
    public interface IStandardModifierCheckerService<TEntity, TMap, T, TDbContext, TFilter> : IModifierCheckerService<TEntity, TMap, T, TDbContext, TFilter>
        where TEntity : StandardModifierChecker<T>
        where TMap : StandardModifierCheckerDto<T>
        where T : IEquatable<T>
        where TDbContext: DbContext
        where TFilter: StandardStatusFilter
    {

    }
    public class StandardModifierCheckerService<TEntity, TMap, T, TDbContext, TFilter> : ModifierCheckerService<TEntity, TMap, T, TDbContext, TFilter>, IStandardModifierCheckerService<TEntity, TMap, T, TDbContext, TFilter>
        where TEntity : StandardModifierChecker<T>
        where TMap : StandardModifierCheckerDto<T>
        where T : IEquatable<T>
        where TDbContext: DbContext
        where TFilter: StandardStatusFilter
    {
        
        public StandardModifierCheckerService(TDbContext context
            //, IAuditTrailService auditTrailService
            ) : base(context)
        {
            
            //_auditTrailService = auditTrailService;
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
        public override IQueryable<TEntity> SearchByFilterModel(TFilter model, IQueryable<TEntity> data = null)
        {
            string status = string.IsNullOrEmpty(model.AuthStatus) ? "U" : model.AuthStatus;
            string name = string.IsNullOrEmpty(model.Name) ? "" : model.Name.ToLower();
            return _context.Set<TEntity>().Where(s => s.AuthStatus == status && s.Name.ToLower().Contains(name));
        }

    }
}
