namespace Inspire.Services.Application.Common
{
    public interface ISortableStandardService<TEntity, TMap, TFilter, T> : ISortableService<TEntity, TMap, TFilter, T>

        where TEntity : ISortableStandard<T>, new()
        where TMap : ISortableStandardDto<T>, new()
        where TFilter : RecordStatusFilter
        where T : IEquatable<T>
    {

    }
    
}
