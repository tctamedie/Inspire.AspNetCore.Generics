using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Inspire.Services.Infrastructure.Common
{
   
    public class StandardModifierCheckerService<TEntity, TMap, T, TDbContext, TFilter>(TDbContext context) : ModifierCheckerService<TEntity, TMap, T, TDbContext, TFilter>(context), IStandardModifierCheckerService<TEntity, TMap, T, TFilter>
        where TEntity : StandardModifierChecker<T>, new()
        where TMap : StandardModifierCheckerDto<T>
        where T : IEquatable<T>
        where TDbContext : DbContext
        where TFilter : StandardStatusFilter
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
            string status = string.IsNullOrEmpty(model.AuthStatus) ? "U" : model.AuthStatus;
            string name = string.IsNullOrEmpty(model.Name) ? "" : model.Name.ToLower();
            return context.Set<TEntity>().Where(s => s.AuthStatus == status && s.Name.ToLower().Contains(name));
        }

    }
}
