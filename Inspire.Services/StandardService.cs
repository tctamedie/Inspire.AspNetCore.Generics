using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Inspire.Services
{
    public interface IStandardService<TEntity, TMap, T, TDbContext, TFilter> : IRecordService<TEntity, TMap, T, TDbContext, TFilter>
        where TEntity : Standard<T>
        where TMap : StandardDto<T>
        where T : IEquatable<T>
        where TDbContext: DbContext
        where TFilter: StandardFilter
    {

    }
    public class StandardService<TEntity, TMap, T, TDbContext, TFilter> : RecordService<TEntity, TMap, T, TDbContext, TFilter>, IStandardService<TEntity, TMap, T, TDbContext, TFilter>
        where TEntity : Standard<T>, new()
        where TMap : StandardDto<T>
        where T : IEquatable<T>
        where TDbContext: DbContext
        where TFilter: StandardFilter
    {
        
        public StandardService(TDbContext context
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
            string name =model is null|| string.IsNullOrEmpty(model.Name) ? "" : model.Name.ToLower();
            if (data is null)
                data = base.SearchByFilterModel(model);
            return data.Where(s=>s.Name.ToLower().Contains(name));
        }


    }
}
