using Inspire.Modeller;
using Inspire.Services.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Inspire.Services.Application.Common
{
    public interface IStandardMakerService<TEntity, TMap, T, TFilter> : IMakerService<TEntity, TMap, T, TFilter>
        where TEntity : IStandardMaker<T>,new()
        where TMap : IStandardMakerDto<T>
        where T : IEquatable<T>
        where TFilter : StandardFilter
    {

    }
    
}
