using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Inspire.DataAccess;
using Inspire.Modeller.Models.Security;

namespace Inspire.Services.Security

{
    public interface IReportGroupServices<TContext> : IStandardService<ReportGroup, ReportGroupDto, string, FilterModel>
        where TContext: RecordContext
    {
        Task<List<ReportGroup>> GetReportGroups(Expression<Func<SubMenu, bool>> match = null);
        ReportGroup GetReportGroup(Expression<Func<SubMenu, bool>> match = null);
    }

    public class ReportGroupRepository : StandardRepository<ReportGroup, ReportGroupDto, string, FilterModel>, IReportGroupServices
    {
        public ReportGroupRepository(WorkforceContext db) : base(db)
        {
        }
        
        public override IQueryable<ReportGroup> GetStatusFilter(FilterModel model)
        {
            var data = db.Set<SubMenu>().Where(s => s.ParentMenu.IsReport).Include(s => s.ParentMenu).ToList();
            var records = CreateTarget<ReportGroup, SubMenu>(data);
            return records.AsQueryable();
        }
        public override Task<List<ReportGroup>> GetListAsync(Expression<Func<ReportGroup, bool>> match = null)
        {
            return base.GetListAsync(match);
        }
        public override async Task<List<ReportGroup>> GetListAsync(FilterModel model)
        {
            var data = await db.Set<SubMenu>().Where(s => s.ParentMenu.IsReport).Include(s => s.ParentMenu).ToListAsync();
            var records = CreateTarget<ReportGroup, SubMenu>(data);
            return records;
        }
        public override IQueryable<ReportGroup> GetSearchFilter(IQueryable<ReportGroup> data, FilterModel model)
        {
            return data;
        }
        public override async Task<List<ReportGroup>> ReadAsync(FilterModel model)
        {
            var data=  await db.Set<SubMenu>().Where(s=>s.ParentMenu.IsReport).Include(s=>s.ParentMenu).ToListAsync();
            var records = CreateTarget<ReportGroup, SubMenu>(data);
            return records;
        }
        public async   Task<List<ReportGroup>> GetReportGroups(Expression<Func<SubMenu, bool>> match = null)
        {
            var data = await db.Set<SubMenu>().Include(s => s.ParentMenu).ToListAsync();
            if (match != null)
                data = await db.Set<SubMenu>().Include(s => s.ParentMenu).Where(match).ToListAsync();
            data = data.Where(s => s.ParentMenu.IsReport).ToList();
            var records =CreateTarget<ReportGroup, SubMenu>(data);
            return records;
        }
        public  ReportGroup GetReportGroup(Expression<Func<SubMenu, bool>> match = null)
        {
            var data = db.Set<SubMenu>().Include(s => s.ParentMenu).FirstOrDefault();
            if (match != null)
                data = db.Set<SubMenu>().Include(s => s.ParentMenu).FirstOrDefault(match);            
            var records = CreateTarget<ReportGroup, SubMenu>(data);
            return records;
        }

    }
}
