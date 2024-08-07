using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Inspire.Services.Infrastructure.Common
{
   
    public class StandardService<TEntity, TMap, T, TDbContext, TFilter>(TDbContext context) : RecordService<TEntity, TMap, T, TDbContext, TFilter>(context), IStandardService<TEntity, TMap, T, TFilter>
        where TEntity : Standard<T>, new()
        where TMap : StandardDto<T>
        where T : IEquatable<T>
        where TDbContext : DbContext
        where TFilter : StandardFilter
    {

        protected override OutputHandler Validate(TMap row, int a = 0, bool c = true, string capturer = "", [CallerMemberName] string caller = "")
        {
            var validation = base.Validate(row, a, c, capturer, caller);
            if (validation.ErrorOccured)
                return validation;

            if (Any(s => s.Name.ToUpper() == row.Name.ToUpper() && !s.Id.Equals(row.Id)))
            {
                return new OutputHandler
                {

                    Description = $" Name {row.Name}for {_modelHeader} already exist"
                };
            }
            return new OutputHandler();

        }
        public override IQueryable<TEntity> SearchByFilterModel(TFilter model, IQueryable<TEntity> data = null)
        {
            string name = model is null || string.IsNullOrEmpty(model.Name) ? "" : model.Name.ToLower();
            if (data is null)
                data = base.SearchByFilterModel(model);
            return data.Where(s => s.Name.ToLower().Contains(name));
        }


    }
}
