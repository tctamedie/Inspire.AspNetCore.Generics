using Inspire;

namespace Inspire.Services.Application.Common
{
    public interface ISortableService<TEntity, TMap, TFilter, T>
        : IMakerCheckerService<TEntity, TMap, T, TFilter>
        where TEntity : ISortableMakerChecker<T>, new()
        where TMap : ISortableMakerCheckerDto<T>, IMakerCheckerDto<T>, new()
        where TFilter : RecordStatusFilter
        where T : IEquatable<T>
    {
        OutputHandler MoveUp(T id);
        OutputHandler MoveDown(T id);
    }
    
}
