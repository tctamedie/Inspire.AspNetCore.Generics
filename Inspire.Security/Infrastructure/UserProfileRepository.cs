namespace Services.SystemSecurity
{
    
    public class UserProfileRepository(SecurityContext db, IProfileMenuRepository profileMenuRepository) : StandardMakerCheckerService<UserProfile, UserProfileDto, string, SecurityContext, StandardStatusFilter>(db), IUserProfileRepository
    {
                
        public async Task<OutputHandler> CopyProfile(string ProfileName, string OldProfileName, string CopyAs, string capturedBy, int authorisers)
        {
            
                if (!string.IsNullOrEmpty(OldProfileName))
                {
                    var records = GetList<UserProfileMenu>().ToList();
                    
                    var Menus = records.Where(s => s.UserProfileId == OldProfileName)
                        .Select(s => new UserProfileMenuDto
                        {
                            SubMenuId = s.SubMenuId,
                            UserProfileId = ProfileName,
                            AccessCode = s.AccessCode
                        }).ToList();
                    return await profileMenuRepository.AddInScopeAsync(Menus,capturedBy,authorisers);
                    
                }
            return new OutputHandler();
        }
        public override IQueryable<UserProfile> SearchByFilterModel(StandardStatusFilter model, IQueryable<UserProfile> data = null)
        {
            string search = model == null || string.IsNullOrEmpty(model.Search) ? "" : model.Search.ToLower();
            return base.SearchByFilterModel(model, data).Where(s => s.Id.ToLower().Contains(search) || s.Name.ToLower().Contains(search));
        }
        protected override OutputHandler Validate(UserProfileDto row, int a = 0, bool c = true, string capturer = "", [CallerMemberName] string caller = "")
        {
            if (string.IsNullOrEmpty(row.Name))
                throw new ValidationException("Profile Name cannot be blank");

            if (Any(s => s.Name.ToUpper() == row.Name.ToUpper() && s.Id.ToUpper() != row.Id.ToUpper()))
            {
                throw new ValidationException("Profile already exists");
            }
            return base.Validate(row, a, c, capturer, caller);
        }
        public List<GenericData<string>> GetRights()
        {
            return RightConfiguration.GetAllowableRights(null);
        }
    }
}