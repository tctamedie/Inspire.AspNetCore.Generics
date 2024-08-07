using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Inspire.Services.Application.Common
{
    public interface IStandardService<TEntity, TMap, T, TFilter> : IRecordService<TEntity, TMap, T, TFilter>
        where TEntity : IStandard<T>
        where TMap : IStandardDto<T>
        where T : IEquatable<T>
        where TFilter : StandardFilter
    {

    }
    
}
