using Inspire.Modeller;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Services
{
    public interface IMakerCheckerService<TEntity, TMap, T, TDbContext, TFilter> : IMakerService<TEntity, TMap, T, TDbContext, TFilter>
        where TEntity : MakerChecker<T>
        where TMap : MakerCheckerDto<T>
        where T : IEquatable<T>
        where TDbContext: DbContext
        where TFilter: RecordStatusFilter
    {
        
    }
    public abstract class  MakerCheckerService<TEntity, TMap, T, TDbContext, TFilter> : MakerService<TEntity, TMap, T, TDbContext, TFilter>, IMakerCheckerService<TEntity, TMap, T, TDbContext, TFilter>
        where TEntity : MakerChecker<T>
        where TMap : MakerCheckerDto<T>
        where T : IEquatable<T>
        where TDbContext: DbContext
        where TFilter: RecordStatusFilter
    {
        
        public MakerCheckerService(TDbContext context
            //, IAuditTrailService auditTrailService
            ):base(context)
        {
            
        }
        protected override bool ValidateAuthoriseOnCreator(T id, string user)
        {
            return !Any(s => s.Id.Equals(id) && s.CreatedBy.ToUpper() == user.ToUpper());
        }
        public override IQueryable<TEntity> SearchByFilterModel(TFilter model, IQueryable<TEntity> data = null)
        {
            string status = string.IsNullOrEmpty(model.AuthStatus) ? "U" : model.AuthStatus;
            return _context.Set<TEntity>().Where(s => s.AuthStatus == status);
        }
        public override Task<List<TEntity>> ReadAsync(TFilter model)
        {
            string status = string.IsNullOrEmpty(model.AuthStatus) ? "A" : model.AuthStatus;
            return _context.Set<TEntity>().Where(s => s.AuthStatus == status).ToListAsync();
        }
        protected override void AppendAuthoriser(TEntity row, string createdBy)
        {
            row.AuthorisedBy = createdBy.ToUpper();
            row.DateAuthorised = DateTime.UtcNow.AddHours(2);
            row.AuthStatus = "A";
        }
        
    }
}
