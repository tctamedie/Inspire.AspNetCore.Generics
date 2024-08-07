namespace Inspire.Services.Infrastructure.Common
{
    public class ToggableStandardService<TEntity, TMap, TFilter, T, TDbContext>(TDbContext db)
        : ToggableService<TEntity, TMap, TFilter, T, TDbContext>(db), IToggableStandardService<TEntity, TMap, TFilter, T>
        where TEntity : ToggableStandard<T>, IToggableStandard<T>, new()
        where TMap : ToggableStandardDto<T>, IToggableStandardDto<T>, new()
        where TFilter : RecordStatusFilter
        where T : IEquatable<T>
        where TDbContext : DbContext
    {
        

        public override Task<OutputHandler> Toggle(T id, string username, int authorisers, bool captureAuditTrail)
        {
            var config = GetClassAttributes<EntityConfiguration, TEntity>(true).FirstOrDefault();
            var record = Find(id);
            if (record == null)
                throw new ValidationException($"{config.Modal} with id {id} does not exist");
            record.IsActive = record.IsActive == null || record.IsActive == false ? true : false;
            var row = CreateDto(record);
            return UpdateAsync(row, username, authorisers, captureAuditTrail);
        }
        protected override OutputHandler Validate(TMap row, int a = 0, bool c = true, string capturer = "", [CallerMemberName] string caller = "")
        {

            if (string.IsNullOrEmpty(row.Name))
                return "Name cannot be blank".Formator(true);
            if (Any(s => s.Name.ToLower().Contains(row.Name.ToLower()) && !s.Id.Equals(row.Id)))
                return "Record Already Exists".Formator(true);
            return base.Validate(row, a, c, caller);
        }
        public override IQueryable<TEntity> SearchByFilterModel(TFilter model, IQueryable<TEntity> data = null)
        {
            string search = model == null || string.IsNullOrEmpty(model.Search) ? "" : model.Search.ToLower();
            var records = base.SearchByFilterModel(model, data);
            return records.Where(s => s.Name.ToLower().Contains(search));
        }
    }
}
