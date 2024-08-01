using Inspire.Annotator;
using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Services
{
    public interface IMakerService<TEntity, TMap, T, TDb, TFilter> : IRecordService<TEntity, TMap, T, TDb, TFilter>
        where TEntity : Maker<T>
        where TMap : MakerDto<T>
        where T : IEquatable<T>
        where TDb: DbContext
        where TFilter: RecordFilter
    {
        
    }
    public abstract class  MakerService<TEntity, TMap, T, TDb, TFilter> : RecordService<TEntity, TMap, T, TDb, TFilter>, IMakerService<TEntity, TMap, T, TDb, TFilter>
        where TEntity : Maker<T>,new()
        where TMap : MakerDto<T>
        where T : IEquatable<T>
        where TDb: DbContext
        where TFilter: RecordFilter
    {
        
        public MakerService(TDb context
            //, IAuditTrailService auditTrailService
            ):base(context)
        {
            
        }
        public override bool ValidateDeleteOnCreator(T id, string user)
        {
            return !Any(s => s.Id.Equals(id) && s.CreatedBy.ToUpper() == user.ToUpper());
        }
        protected override void AppendCreator(TEntity row, string createdBy)
        {
            row.CreatedBy = createdBy.ToUpper();
            row.DateCreated = DateTime.UtcNow.AddHours(2);
        }
        
    }
}
