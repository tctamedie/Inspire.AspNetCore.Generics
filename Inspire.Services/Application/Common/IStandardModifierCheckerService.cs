using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Inspire.Services.Application.Common
{
    public interface IStandardModifierCheckerService<TEntity, TMap, T, TFilter> : IModifierCheckerService<TEntity, TMap, T, TFilter>
        where TEntity : IStandardModifierChecker<T>, new()
        where TMap : IStandardModifierCheckerDto<T>
        where T : IEquatable<T>
        where TFilter : StandardStatusFilter
    {

    }
    
}
