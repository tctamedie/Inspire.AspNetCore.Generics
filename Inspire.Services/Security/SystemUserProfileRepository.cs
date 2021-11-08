using Inspire.Annotator.Annotations;
using Inspire.DataAccess.Security;
using Inspire.Modeller;
using Inspire.Modeller.Models.Security;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Services.Security
{
    public class SystemUserProfileFilter: RecordStatusFilter
    {
        [TableFilter(2,  ControlType: ControlType.Hidden)]
        public string UserID { get; set; }
    }
    public interface ISystemUserProfileService<TContext> : IMakerCheckerService<SystemUserProfile, SystemUserProfileDto, string, TContext, SystemUserProfileFilter>
        where TContext: SecurityContext
    {

    }
    public class SystemUserProfileService<TContext> : MakerCheckerService<SystemUserProfile, SystemUserProfileDto, string, TContext, SystemUserProfileFilter>, ISystemUserProfileService<TContext>
        where TContext : SecurityContext
    {
        
        public SystemUserProfileService(TContext db) : base(db)
        {
        }
        public override Task<OutputModel> AddAsync(SystemUserProfileDto row, string createBy)
        {
            var recordCount = Count(s => s.UserID == row.UserID) + 1;
            row.Id = $"{row.UserID}{recordCount:00}";
            return base.AddAsync(row, createBy);
        }
        public override IQueryable<SystemUserProfile> SearchByFilterModel(SystemUserProfileFilter model, IQueryable<SystemUserProfile> data = null)
        {
            string search = GetSearchString(model);
            if (data == null)
                data = base.SearchByFilterModel(model, data);
            return data.Where(s => (s.UserProfile.Name.ToLower().Contains(search) || s.UserProfileID.Contains(search)) && s.UserID == model.UserID).Include(s => s.UserProfile).Include(s => s.SystemUser); ;
        }
        
        public override SystemUserProfile Find(params object[] id)
        {
            if (id.Length == 0)
                return null;
            string systemUserProfileID = id[0].ToString();
            return _context.Set<SystemUserProfile>().Include(s => s.UserProfile).Include(s => s.SystemUser).FirstOrDefault(s => s.Id == systemUserProfileID);
        }
        
        

    }
}
