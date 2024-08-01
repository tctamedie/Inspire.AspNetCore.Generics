using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Inspire.Services
{
    public interface IStandardMakerService<TEntity, TMap, T, TDbContext, TFilter> : IMakerService<TEntity, TMap, T, TDbContext, TFilter>
        where TEntity : StandardMaker<T>
        where TMap : StandardMakerDto<T>
        where T : IEquatable<T>
        where TDbContext: DbContext
        where TFilter: StandardFilter
    {

    }
    /// <summary>
    /// Supports Crud operations on the database for a given standard entity which tracks only creators
    /// </summary>
    /// <typeparam name="TEntity">Persistable Database Entity</typeparam>
    /// <typeparam name="TMap">the data transfer object for the database entity</typeparam>
    /// <typeparam name="T">the primary key datatatype for the entity</typeparam>
    /// <typeparam name="TDbContext">the database context to use when doing crud operations</typeparam>
    /// <typeparam name="TFilter">the model to use when creating table filters</typeparam>
    public class StandardMakerService<TEntity, TMap, T, TDbContext, TFilter> : MakerService<TEntity, TMap, T, TDbContext, TFilter>, IStandardMakerService<TEntity, TMap, T, TDbContext, TFilter>
        where TEntity : StandardMaker<T>, new()
        where TMap : StandardMakerDto<T>
        where T : IEquatable<T>
        where TDbContext: DbContext
        where TFilter: StandardFilter
    {
        
        public StandardMakerService(TDbContext context
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
            string name = model==null||string.IsNullOrEmpty(model.Name) ? "" : model.Name.ToLower();
            return _context.Set<TEntity>().Where(s => s.Name.ToLower().Contains(name));
        }

    }
}
