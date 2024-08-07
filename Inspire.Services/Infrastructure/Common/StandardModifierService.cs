

namespace Inspire.Services.Infrastructure.Common
{
    
    public class StandardModifierService<TEntity, TMap, T, TDbContext, TFilter>(TDbContext context) : ModifierService<TEntity, TMap, T, TDbContext, TFilter>(context), IStandardModifierService<TEntity, TMap, T, TFilter>
        where TEntity : StandardModifier<T>, new()
        where TMap : StandardModifierDto<T>
        where T : IEquatable<T>
        where TDbContext : DbContext
        where TFilter : StandardFilter
    {

        
        public override IQueryable<TEntity> SearchByFilterModel(TFilter model, IQueryable<TEntity> data = null)
        {
            string name = string.IsNullOrEmpty(model.Name) ? "" : model.Name.ToLower();
            return context.Set<TEntity>().Where(s => s.Name.ToLower().Contains(name));
        }
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


    }
}
