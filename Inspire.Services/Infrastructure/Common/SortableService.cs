using Inspire;
using Inspire.Services.Application.Common;

namespace Inspire.Services.Infrastructure.Common
{
    
    public class SortableService<TEntity, TMap, TFilter, T, TDbContext>(TDbContext db)
        : MakerCheckerService<TEntity, TMap, T, TDbContext, TFilter>(db), ISortableService<TEntity, TMap, TFilter, T>
        where TEntity : SortableMakerChecker<T>, ISortableMakerChecker<T>, IMakerChecker<T>, new()
        where TMap : SortableMakerCheckerDto<T>, ISortableMakerCheckerDto<T>, new()
        where TFilter : RecordStatusFilter
        where TDbContext : DbContext
        where T : IEquatable<T>
    {
        protected IQueryable<TEntity> query;
        
        public OutputHandler MoveUp(T id)
        {
            var query = Filter(id);
            var record = query.FirstOrDefault(s => s.Id.Equals(id));
            if (record == null)
                throw new ValidationException($"{_modelHeader} is already at the top");
            if (record.Order > 1)
            {
                var lowerRecordsToReplace = query.Where(s => s.Order == record.Order - 1).ToList();
                record.Order = record.Order - 1;
                lowerRecordsToReplace.ForEach(s =>
                {
                    s.Order = record.Order;
                });
                db.SaveChanges();
            }
            else
            {
                throw new ValidationException($"{_modelHeader} is already at the top");
            }
            return $" Successfully Moved {_modelHeader}  with id {id} One step up".Formator();
        }
        public virtual IQueryable<TEntity> Filter(T id)
        {
            return query;
        }
        public OutputHandler MoveDown(T id)
        {
            var query = Filter(id);
            var record = query.FirstOrDefault(s => s.Id.Equals(id));
            if (record == null)
                throw new ValidationException($"{_modelHeader} is already at the top");
            if (record.Order < query.Max(s => s.Order))
            {
                var lowerRecordsToReplace = query.Where(s => s.Order == record.Order + 1).ToList();
                record.Order = record.Order + 1;
                lowerRecordsToReplace.ForEach(s =>
                {
                    s.Order = record.Order;
                });
                db.SaveChanges();
            }
            else
            {
                throw new ValidationException($"{_modelHeader} is already at the bottom");
            }
            return $" Successfully Moved {_modelHeader}  with id {id} One step down".Formator();
        }
        public override IQueryable<TEntity> SearchByFilterModel(TFilter model, IQueryable<TEntity> data = null)
        {
            return base.SearchByFilterModel(model, data).OrderBy(s => s.Order);
        }
    }
}
