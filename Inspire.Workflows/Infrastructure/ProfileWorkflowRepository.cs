namespace Services.SystemSecurity
{
    public class ProfileWorkflowRight
    {
        public LoanStage LoanStage { get; set; }
        public string Profile { get; set; }

        public int MenuId { get; set; }
    }

    public interface IProfileWorkflowRepository : IToggableService<UserProfileWorkflowRight, UserProfileWorkflowRightDto, WorkflowFilterModel, int>
    {
        List<GenericData<LoanStage>> GetAllowableRights(int menuId);
        List<GenericData<int>> GetAllowableMenus(int menuId);
        List<GenericData<int>> GetWorkflowMenus();
        /// <summary>
        /// Get number of people to authorise a record at a given workflow stage
        /// </summary>
        /// <param name="row"> The input indicating Profile, MenuId and Stage</param>
        /// <returns>The number of allowable authorisers</returns>
        int GetAuthorisers(ProfileWorkflowRight row);
    }
    public class ProfileWorkflowRepository : ToggableService<UserProfileWorkflowRight, UserProfileWorkflowRightDto, WorkflowFilterModel, int>, IProfileWorkflowRepository
    {

        IMenuSettingRepository _menuSettingRepository;
        IParentMenuSettingRepository parentMenuSettingRepository;
        public ProfileWorkflowRepository(
            PayPlusEntities db,
            IParentMenuSettingRepository parentMenuSettingRepository,
            IMenuSettingRepository menuSettingRepository,
            IOptions<ApplicationEnvironment> options) : base(db, options)
        {
            _menuSettingRepository = menuSettingRepository;
            this.parentMenuSettingRepository = parentMenuSettingRepository;
        }
        public List<GenericData<LoanStage>> GetAllowableRights(int menuId)
        {
            return GenericListFromEnum<LoanStage>();
        }
        public List<GenericData<int>> GetAllowableMenus(int menuId)
        {
            return _menuSettingRepository.GetAllowableMenus(menuId);
        }
        public List<GenericData<int>> GetWorkflowMenus()
        {
            return parentMenuSettingRepository.GetWOrkflows();
        }
        protected override UserProfileWorkflowRightDto AlignDataTransferObject(UserProfileWorkflowRightDto row, [CallerMemberName] string caller = "")
        {
            if (row.Id == 0)
            {
                var count = GetList<UserProfileWorkflowRight>();
                if (count.Count > 0)
                {
                    var max = count.Max(s => s.Id);
                    row.Id = max + 1;
                }
            }
            row.IsActive = true;
            return row;
        }



        public override OutputHandler Validate(UserProfileWorkflowRightDto row, int authorisers = 0, bool captureTrail = true, string capturer = "", [CallerMemberName] string caller = "")
        {

            if (row.LoanStage == 0)
                return "Please Check at least one Workflow right for this profile on this menu".Formator(true);
            return RecordExists(s => s.ProfileName == row.ProfileName && s.MenuID == row.MenuID && s.LoanStage == row.LoanStage && s.Id != row.Id);
            //return base.Validate(row, authorisers, captureTrail, caller);
        }

        protected override IQueryable<UserProfileWorkflowRight> Search(WorkflowFilterModel model, IQueryable<UserProfileWorkflowRight> data = null)
        {

            var profile = model == null || string.IsNullOrEmpty(model.ProfileName) ? "" : model.ProfileName;
            var parentMenu = model == null ? 0 : model.ParentMenuId;
            var search = GetSearchString(model);
            return base.Search(model, data).Where(s =>
            s.SubMenuItem.ParentMenuID == parentMenu
            && s.ProfileName == profile
            && (s.SubMenuItem.Name).ToLower().Contains(search)
            ).Include(s => s.SubMenuItem);
        }

        public int GetAuthorisers(ProfileWorkflowRight row)
        {
            if (row == null)
                throw new ValidationException("Profile not set at the stage");

            var rows = db.Set<UserProfileWorkflowRight>()
                .Where(s => s.LoanStage == row.LoanStage && s.MenuID == row.MenuId).ToList();

            if (rows.Count == 1)
            {
                return rows.FirstOrDefault().Authorisers;
            }

            var profileWorkflowRight = rows.Where(s => s.ProfileName == row.Profile).FirstOrDefault();

            if (profileWorkflowRight == null)
                throw new ValidationException("Authorisers not set at the stage");

            return profileWorkflowRight.Authorisers;

        }
    }
}