namespace Inspire.Services.Application.Common
{
    public interface IToggableStandardService<TEntity, TMap, TFilter, T> : IToggableService<TEntity, TMap, TFilter, T>

        where TEntity : IToggableStandard<T>, new()
        where TMap : IToggableStandardDto<T>, new()
        where TFilter : RecordStatusFilter
        where T : IEquatable<T>
    {

    }
    
}
