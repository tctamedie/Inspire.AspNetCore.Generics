using Inspire.Annotator;
using Inspire.Modeller;
using Inspire.Services.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Services.Application.Common
{

    public interface IModifierService<TEntity, TMap, T, TFilter> : IMakerService<TEntity, TMap, T, TFilter>
        where TEntity : IModifier<T>, new()
        where TMap : IModifierDto<T>
        where T : IEquatable<T>
        where TFilter : RecordFilter
    {

    }
    
}
