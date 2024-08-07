namespace Inspire.Security.Infrastructure;

public class SystemUserProfileRepository(SecurityContext db) : ToggableService<SystemUserProfile, SystemUserProfileDto, UserFilterModel, string, SecurityContext>(db), ISystemUserProfileRepository
{

    protected override OutputHandler Validate(SystemUserProfileDto row, int authorisers = 0, bool captureTrail = true, string capturer = "", [CallerMemberName] string caller = "")
    {
        if (string.IsNullOrEmpty(row.UserProfileID))
            return "Please Select Proflie".Formator(true);
        if (string.IsNullOrEmpty(row.UserID))
            return "Please Select User".Formator(true);
        if (Any(s => s.UserID == row.UserID && s.UserProfileID == row.UserProfileID && s.Id != row.Id))
            return "Profile already exist for user".Formator(true);
        return base.Validate(row, authorisers, captureTrail, caller);
    }
    protected override SystemUserProfileDto AlignDataTransferObject(SystemUserProfileDto row, [CallerMemberName] string caller = "")
    {

        row.IsActive = true;
        row.Id = $"{row.UserProfileID}{row.UserID}";
        return row;
    }
    public override IQueryable<SystemUserProfile> SearchByFilterModel(UserFilterModel model, IQueryable<SystemUserProfile> data = null)
    {
        var user = model == null ? "" : model.UserName;
        var search = GetSearchString(model);
        return base.SearchByFilterModel(model, data).Where(s => s.UserProfile.Name.ToLower().Contains(search) && s.UserID
        == user).Include(s => s.UserProfile);
    }
    public async Task<OutputHandler> CopyUser(string copyFrom, string copyTo, string capturedBy, int authorisers, bool captureTrail)
    {

        if (!string.IsNullOrEmpty(copyFrom))
        {
            var existingProfiles = GetList(s => s.UserID == copyTo);
            db.Set<SystemUserProfile>().RemoveRange(existingProfiles);

            var OtherUserProfiles = GetList(s => s.UserID == copyFrom)
                .Select(s => new SystemUserProfileDto { UserID = copyTo, UserProfileID = s.ProfileName, IsActive = true, Id = $"{s.UserProfileID}{copyTo}" }).ToList();

            return await UpdateInScopeAsync(OtherUserProfiles, capturedBy, authorisers, captureTrail);
        }
        return new OutputHandler();
    }

}