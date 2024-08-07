using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Inspire.Services.Application.Common
{
    public interface IStandardModifierService<TEntity, TMap, T, TFilter> : IModifierService<TEntity, TMap, T, TFilter>
        where TEntity : IStandardModifier<T>,new()
        where TMap : IStandardModifierDto<T>
        where T : IEquatable<T>
        where TFilter : StandardFilter
    {

    }
    
}
