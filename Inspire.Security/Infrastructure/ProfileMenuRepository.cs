namespace Inspire.Security.Infrastructure;


public class ProfileMenuRepository(SecurityContext db) : ToggableService<UserProfileMenu, UserProfileMenuDto, RightFilterModel, string, SecurityContext>(db), IProfileMenuRepository
{

    IMenuSettingRepository _menuSettingRepository;

    public List<GenericData<string>> GetAllowableRights(string menuId)
    {
        return _menuSettingRepository.GetAllowableRights(menuId);
    }
    public List<GenericData<string>> GetAllowableMenus(string menuId)
    {
        return _menuSettingRepository.GetAllowableMenus(menuId);
    }
    protected override UserProfileMenuDto AlignDataTransferObject(UserProfileMenuDto row, [CallerMemberName] string caller = "")
    {
       
        row.Id = $"{row.UserProfileId}{row.SubMenuId}";
        row.IsActive = true;
        return row;
    }


    protected override OutputHandler Validate(UserProfileMenuDto row, int authorisers = 0, bool captureTrail = true, string capturer = "", [CallerMemberName] string caller = "")
    {

        if (string.IsNullOrEmpty(row.AccessCode))
            return "Please Check at least one access right for this profile on this menu".Formator(true);
        if (Any(s => s.UserProfileId == row.UserProfileId && s.UserProfileId == row.UserProfileId && s.Id != row.Id))
            return "The record Already Exists".Formator(true);
        return base.Validate(row, authorisers, captureTrail, caller);
    }

    public override IQueryable<UserProfileMenu> SearchByFilterModel(RightFilterModel model, IQueryable<UserProfileMenu> data = null)
    {

        var profile = model == null || string.IsNullOrEmpty(model.ProfileName) ? "" : model.ProfileName;
        var parentMenu = model == null ? "" : model.ParentMenuId;
        var search = GetSearchString(model);
        return base.SearchByFilterModel(model, data).Where(s =>
        s.SubMenu.ParentMenuID == parentMenu
        && s.UserProfileId == profile
        && (s.SubMenu.Name).ToLower().Contains(search)
        ).Include(s => s.SubMenu);
    }
}