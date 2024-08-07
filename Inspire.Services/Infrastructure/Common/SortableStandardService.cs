using Inspire.Services.Application.Common;

namespace Inspire.Services.Infrastructure.Common
{
    
    public class SortableStandardService<TEntity, TMap, TFilter, T, TDbContext>(TDbContext db)
        : SortableService<TEntity, TMap, TFilter, T, TDbContext>(db), ISortableStandardService<TEntity, TMap, TFilter, T>
        where TEntity : SortableStandard<T>, ISortableStandard<T>, new()
        where TMap : SortableStandardDto<T>, ISortableStandardDto<T>, new()
        where TFilter : RecordStatusFilter
        where T : IEquatable<T>
        where TDbContext : DbContext
    {
        

        protected override OutputHandler Validate(TMap row, int a = 0, bool c = true, string capturer = "", [CallerMemberName] string caller = "")
        {

            if (string.IsNullOrEmpty(row.Name))
                return "Name cannot be blank".Formator(true);
            if (Any(s => s.Name.ToLower().Contains(row.Name.ToLower()) && !s.Id.Equals(row.Id)))
                return "Record Already Exists".Formator(true);
            return base.Validate(row, a, c, caller);
        }
        public override IQueryable<TEntity> SearchByFilterModel (TFilter model, IQueryable<TEntity> data = null)
        {
            string search = model == null || string.IsNullOrEmpty(model.Search) ? "" : model.Search.ToLower();
            var records = base.SearchByFilterModel(model, data);
            return records.Where(s => s.Name.ToLower().Contains(search));
        }
    }
}
