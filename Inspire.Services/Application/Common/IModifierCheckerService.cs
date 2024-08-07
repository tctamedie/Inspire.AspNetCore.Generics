using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Services.Application.Common
{
    public interface IModifierCheckerService<TEntity, TMap, T, TFIlter> : IModifierService<TEntity, TMap, T, TFIlter>
        where TEntity : IModifierChecker<T>, new()
        where TMap : IModifierCheckerDto<T>
        where T : IEquatable<T>
        where TFIlter : RecordStatusFilter
    {

    }
    
}
