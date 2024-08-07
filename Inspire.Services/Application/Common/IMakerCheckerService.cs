using Microsoft.EntityFrameworkCore;

namespace Inspire.Services.Application.Common
{
    public interface IMakerCheckerService<TEntity, TMap, T, TFilter> : IMakerService<TEntity, TMap, T, TFilter>
        where TEntity : IMakerChecker<T>, new()
        where TMap : IMakerCheckerDto<T>
        where T : IEquatable<T>
        where TFilter : RecordStatusFilter
    {

    }

}
