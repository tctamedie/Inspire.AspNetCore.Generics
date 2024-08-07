using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Inspire.Services.Application.Common
{
    public interface IStandardMakerCheckerService<TEntity, TMap, T, TFilter> : IMakerCheckerService<TEntity, TMap, T, TFilter>
        where TEntity : IStandardMakerChecker<T>, new()
        where TMap : IStandardMakerCheckerDto<T>
        where T : IEquatable<T>
        where TFilter : StandardStatusFilter
    {

    }
    
}
