using Inspire.DataAccess.Security;
using Inspire.Modeller;
using Inspire.Modeller.Models.Security;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Inspire.Services.Security
{
    public class UserProfileMenuFilter : RecordStatusFilter
    {
        [TableFilter(2, Width: 3, Name: "Parent", ControlType: ControlType.Hidden )]
        public string FilterUserProfileID { get; set; }
        [TableFilter(2, Width: 3, Name: "Parent Menu")]
        [List("ParentMenu", Area: "Security")]
        public string FilterParentMenuID { get; set; }
    }
    public interface IUserProfileMenuService<TContext>: IModifierCheckerService<UserProfileMenu, UserProfileMenuDto, string, TContext, UserProfileMenuFilter>
        where TContext: SecurityContext
    {
        List<SubMenu> GetSubMenus(string parentMenuID);
    }

    public class UserProfileMenuService<TContext> : ModifierCheckerService<UserProfileMenu, UserProfileMenuDto, string, TContext, UserProfileMenuFilter>, IUserProfileMenuService<TContext>
        where TContext: SecurityContext
    {
        public override Task<OutputModel> AddAsync(UserProfileMenuDto row, string createBy)
        {
            if (string.IsNullOrEmpty(row.Id))
            {
                row.Id = $"{row.UserProfileID}{row.SubMenuID}";
            }
            return base.AddAsync(row, createBy);
        }
        
        protected override OutputModel Validate(UserProfileMenuDto row, [CallerMemberName] string caller = null)
        {
            if (string.IsNullOrEmpty(row.SubMenuID))
                return new OutputModel(true) { Message = "Please Select Sub Menu" };
            if (string.IsNullOrEmpty(row.UserProfileID))
                return new OutputModel(true) { Message = "Please Select User Profile" };
            return base.Validate(row, caller);
        }
        public List<SubMenu> GetSubMenus(string parentMenuID)
        {
            return _context.Set<SubMenu>().Where(s => s.ParentMenuID == parentMenuID).ToList();
        }
        public UserProfileMenuService(TContext db) : base(db)
        {
        }
        public override UserProfileMenu Find(params object[] id)
        {
            var upID = id[0].ToString();
            return _context.Set<UserProfileMenu>().Where(s=>s.Id==upID).Include(s => s.SubMenu).ThenInclude(s => s.ParentMenu).FirstOrDefault();
        }
        public override IQueryable<UserProfileMenu> SearchByFilterModel(UserProfileMenuFilter model, IQueryable<UserProfileMenu> data = null)
        {
            string search = model == null ? "" : string.IsNullOrEmpty(model.Search) ? "" : model.Search;
            string parentMenuID = model == null ? "" : string.IsNullOrEmpty(model.FilterParentMenuID) ? "" : model.FilterParentMenuID;
           
            return _context.Set<UserProfileMenu>().Where(s => s.SubMenu.Name.ToUpper().Contains(search) && s.SubMenu.ParentMenuID == parentMenuID && s.UserProfileID == model.FilterUserProfileID)
                .Include(s => s.UserProfile).Include(s => s.SubMenu).ThenInclude(s => s.ParentMenu);
        }
        
        

    }
}
