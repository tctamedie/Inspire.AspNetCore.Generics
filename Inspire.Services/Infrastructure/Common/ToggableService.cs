namespace Inspire.Services.Infrastructure.Common
{
    public class ToggableService<TEntity, TMap, TFilter, T, TDbContext>(TDbContext db)
        : MakerCheckerService<TEntity, TMap, T, TDbContext, TFilter>(db), IToggableService<TEntity, TMap, TFilter, T>
        where TEntity : ToggableMakerChecker<T>, IToggableMakerChecker<T>, IMakerChecker<T>, new()
        where TMap : ToggableMakerCheckerDto<T>, IToggableMakerCheckerDto<T>, new()
        where TFilter : RecordStatusFilter
        where T : IEquatable<T>
        where TDbContext : DbContext
    {
        
        protected virtual OutputHandler ManageToggle(TEntity record)
        {
            record.IsActive = record.IsActive == null || record.IsActive == false ? true : false;
            return new OutputHandler();
        }
        protected virtual void ValidateToggle(TEntity record, EntityConfiguration config)
        {
            if (record == null)
                throw new ValidationException($"{config.Modal} with id {record.Id} does not exist");
        }
        public virtual Task<OutputHandler> Toggle(T id, string username, int authorisers, bool captureAuditTrail)
        {
            var config = GetClassAttributes<EntityConfiguration, TEntity>(true).FirstOrDefault();
            var record = Find(id);
            ValidateToggle(record, config);
            OutputHandler output = ManageToggle(record);
            if (output.ErrorOccured)
                return Task.FromResult(output);
            var row = CreateDto(record);
            return UpdateAsync(row, username, authorisers, captureAuditTrail);
        }
        
    }
}
