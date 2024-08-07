using Inspire.DataAccess.Security;
using Inspire.Modeller;
using Inspire.Modeller.Models.Security;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Services.Security
{
    public class UserProfileReportFilter: RecordStatusFilter
    {
        [TableFilter(Order:1, ControlType: ControlType.Hidden)]
        public string UserProfileID { get; set; }
    }
    public interface IUserProfileReportService<TContext>: IModifierCheckerService<UserProfileReport, UserProfileReportDto, string, TContext, UserProfileReportFilter>
        where TContext: SecurityContext
    {

    }

    public class UserProfileReportService<TContext> : ModifierCheckerService<UserProfileReport,UserProfileReportDto, string, TContext, UserProfileReportFilter>, IUserProfileReportService<TContext>
        where TContext : SecurityContext
    {
        
        public UserProfileReportService(TContext db) : base(db)
        {
        }
        public override Task<OutputModel> AddAsync(UserProfileReportDto row, string createBy)
        {
            var recordCount = Count(s => s.UserProfileID == row.UserProfileID) + 1;
            row.Id = $"{row.UserProfileID}{recordCount:0000}";
            return base.AddAsync(row, createBy);
        }
        
        public override UserProfileReport Find(params object[] id)
        {
            if (id.Length == 0)
                return null;
            string userProfileMenuID = id[0].ToString();
            return _context.Set<UserProfileReport>().Include(s => s.UserProfile).Include(s => s.Report).ThenInclude(s => s.SubMenu).FirstOrDefault(s => s.Id == userProfileMenuID);
        }
        public override IQueryable<UserProfileReport> SearchByFilterModel(UserProfileReportFilter model, IQueryable<UserProfileReport> data = null)
        {
            string search = model == null ? "" : string.IsNullOrEmpty(model.Search) ? "" : model.Search;
            return _context.Set<UserProfileReport>().Include(s => s.UserProfile).Include(s => s.Report).ThenInclude(s => s.SubMenu).Where(s => s.Report.Name.ToLower().Contains(search) && s.UserProfileID == model.UserProfileID);
        }
        public override string GetPrependedHeader(string foreignKey)
        {
            var row = _context.Set<UserProfile>().Find(foreignKey);
            if (row == null)
                return "";
            return row.Name;
        }

    }
}
