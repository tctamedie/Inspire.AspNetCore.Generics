using Inspire.Annotator;
using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Services.Application.Common
{
    public interface IMakerService<TEntity, TMap, T, TFilter> : IRecordService<TEntity, TMap, T, TFilter>
        where TEntity : IMaker<T>
        where TMap : IMakerDto<T>
        where T : IEquatable<T>
        where TFilter : RecordFilter
    {

    }

}
