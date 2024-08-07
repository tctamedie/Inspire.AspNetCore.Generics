
namespace Inspire.Services.Infrastructure.Common
{
    
    /// <summary>
    /// Supports Crud operations on the database for a given standard entity which tracks only creators
    /// </summary>
    /// <typeparam name="TEntity">Persistable Database Entity</typeparam>
    /// <typeparam name="TMap">the data transfer object for the database entity</typeparam>
    /// <typeparam name="T">the primary key datatatype for the entity</typeparam>
    /// <typeparam name="TDbContext">the database context to use when doing crud operations</typeparam>
    /// <typeparam name="TFilter">the model to use when creating table filters</typeparam>
    public class StandardMakerService<TEntity, TMap, T, TDbContext, TFilter>(TDbContext context) : MakerService<TEntity, TMap, T, TDbContext, TFilter>(context), IStandardMakerService<TEntity, TMap, T, TFilter>
        where TEntity : StandardMaker<T>, new()
        where TMap : StandardMakerDto<T>
        where T : IEquatable<T>
        where TDbContext : DbContext
        where TFilter : StandardFilter
    {



        protected override OutputHandler Validate(TMap row, int a = 0, bool c = true, string capturer = "", [CallerMemberName] string caller = "")
        {
            var validation = base.Validate(row, a, c, capturer, caller);
            if (validation.ErrorOccured)
                return validation;

            if (Any(s => s.Name.ToUpper() == row.Name.ToUpper() && !s.Id.Equals(row.Id)))
            {
                return new OutputHandler
                {

                    Description = $" Name {row.Name}for {_modelHeader} already exist"
                };
            }
            return new OutputHandler();

        }
        public override IQueryable<TEntity> SearchByFilterModel(TFilter model, IQueryable<TEntity> data = null)
        {
            string name = model == null || string.IsNullOrEmpty(model.Name) ? "" : model.Name.ToLower();
            return context.Set<TEntity>().Where(s => s.Name.ToLower().Contains(name));
        }

    }
}
