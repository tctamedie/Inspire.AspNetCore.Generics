using Microsoft.EntityFrameworkCore;
using Inspire.Services;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Inspire.Modeller;
using Inspire.Annotator.Annotations;
using Inspire.Modeller.Models.Security;
using Inspire.DataAccess.Security;

namespace Services.Administration
{
    public class ReportFilter: StandardFilter
    {
        [TableFilter(1)]
        public string SubMenuID { get; set; }
    }
    public interface IReportService<TContext> : IStandardService<Report, ReportDto, string,TContext, ReportFilter>
        where TContext: SecurityContext
    {
        //Task<List<ReportTab>> GetReportTabs(string reportID);
        Task DeleteReportTabs(string reportID);
        //Task AddReportTabs(List<ReportTab> data);
        Task<List<InputField>> GetInputFields();
    }
    public class ReportService<TContext> : StandardService<Report, ReportDto, string, TContext, ReportFilter>, IReportService<TContext>
        where TContext: SecurityContext
    {
        public ReportService(TContext db) : base(db)
        {
        }
        public override Report Find(params object[] id)
        {
            var reportID = id[0].ToString();
            return _context.Set<Report>().Where(s => s.Id == reportID).Include(s => s.SubMenu).FirstOrDefault();
        }
        public override IQueryable<Report> SearchByFilterModel(ReportFilter model, IQueryable<Report> data = null)
        {
            string search = GetSearchString(model);
            data = base.SearchByFilterModel(model, data);
            return data.Where(s => s.SubMenuID == model.SubMenuID).Include(s => s.SubMenu).ThenInclude(s => s.ParentMenu);
        }
        
        
        public Task<List<ReportTab>> GetReportTabs(string reportID)
        {
            return _context.Set<ReportTab>().Where(s => s.ReportID == reportID).Include(s => s.ReportRows).ThenInclude(s => s.ReportFields).ToListAsync();
        }
        public async Task DeleteReportTabs(string reportID)
        {
            var data = await GetReportTabs(reportID);
            _context.RemoveRange(data);
            await _context.SaveChangesAsync();
        }
        public async Task AddReportTabs(List<ReportTab> data)
        { 
            await _context.AddRangeAsync(data);
            await _context.SaveChangesAsync();
        }
        public Task<List<InputField>> GetInputFields()
        {
            return _context.Set<InputField>().ToListAsync();
        }
        public override Task<OutputModel> AddAsync(ReportDto row, string createBy)
        {
            var count = Count(s => s.SubMenuID == row.SubMenuID) + 1;
            string id = $"{row.SubMenuID}{count:000}";
            while (Any(s => s.Id == id))
            {
                count++;
                id = $"{row.SubMenuID}{count:000}";
            }
            row.Id = id;
            return base.AddAsync(row, createBy);
        }
        
        protected override OutputModel Validate(ReportDto row, [CallerMemberName] string caller = null)
        {
            
            if (caller.ToLower().StartsWith("add") || caller.ToLower().StartsWith("edit"))
            {
                if (Any(s => !(s.Id.Equals(row.Id))&&s.SubMenuID==row.SubMenuID && s.Name.ToUpper() == row.Name.ToUpper()))
                    return new OutputModel(true)
                    {
                        Message = $" Name {row.Name}for {_modelHeader} already exist"
                    };
            }
            return base.Validate(row, caller);
           
        }

    }
}
