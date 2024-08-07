using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Inspire.Services.Infrastructure.Common
{
    
    public class StandardMakerCheckerService<TEntity, TMap, T, TDbContext, TFilter>(TDbContext context) : MakerCheckerService<TEntity, TMap, T, TDbContext, TFilter>(context), IStandardMakerCheckerService<TEntity, TMap, T, TFilter>
        where TEntity : StandardMakerChecker<T>, new()
        where TMap : StandardMakerCheckerDto<T>
        where T : IEquatable<T>
        where TDbContext : DbContext
        where TFilter : StandardStatusFilter
    {
        protected override OutputHandler Validate(TMap row, int a = 0, bool c = true, string capturer = "", [CallerMemberName] string caller = "")
        {
            var validation = base.Validate(row,a,c, capturer, caller);
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
            string status = model == null || string.IsNullOrEmpty(model.AuthStatus) ? "U" : model.AuthStatus;
            string name = model == null || string.IsNullOrEmpty(model.Name) ? "" : model.Name.ToLower();
            return context.Set<TEntity>().Where(s => s.AuthStatus == status && s.Name.ToLower().Contains(name));
        }



    }
}
