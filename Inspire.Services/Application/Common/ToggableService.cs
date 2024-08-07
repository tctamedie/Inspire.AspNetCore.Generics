namespace Inspire.Services.Application.Common
{
    public interface IToggableService<TEntity, TMap, TFilter, T>
        : IMakerCheckerService<TEntity, TMap, T, TFilter>
        where TEntity : IToggableMakerChecker<T>, new()
        where TMap : IToggableMakerCheckerDto<T>, IMakerCheckerDto<T>, new()
        where TFilter : RecordStatusFilter
        where T : IEquatable<T>
    {
        Task<OutputHandler> Toggle(T id, string username, int authorisers, bool captureAuditTrail);
    }
    
}
