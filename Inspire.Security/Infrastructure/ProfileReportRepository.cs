namespace Inspire.Security.Infrastructure;


public class ProfileReportRepository(SecurityContext db, IReportService _reportRepository) : ToggableService<ProfileReport, ProfileReportDto, ReportGroupFilterModel, int, SecurityContext>(db), IProfileReportRepository
{
    
    public List<GenericData<string>> GetAllowableReports(string menuId)
    {
        return _reportRepository.GetReports(menuId);
    }
    public List<GenericData<string>> GetAllowableFormats(int menuId)
    {
        return _reportRepository.GetAllowableFormats(menuId);
    }
    public List<GenericData<string>> GetReportGroups()
    {
        return _reportRepository.GetReportGroups();
    }

    protected override ProfileReportDto AlignDataTransferObject(ProfileReportDto row, [CallerMemberName] string caller = "")
    {
        row.IsActive = true;

        return base.AlignDataTransferObject(row, caller);
    }

    protected override OutputHandler Validate(ProfileReportDto row, int authorisers = 0, bool captureTrail = true, string capturer = "", [CallerMemberName] string caller = "")
    {

        if (string.IsNullOrEmpty(row.ProfileFormats))
            throw new ValidationException( "Please select atleast one report format for this profile on this report");
        if (Any(s => s.ProfileID == row.ProfileID && s.ReportID == row.ReportID && s.Id != row.Id))
        {
            throw new ValidationException("The record already exists");
        }
        return base.Validate(row, authorisers, captureTrail, caller);
    }

    public override IQueryable<ProfileReport> SearchByFilterModel(ReportGroupFilterModel model, IQueryable<ProfileReport> data = null)
    {

        var profile = model == null || string.IsNullOrEmpty(model.ProfileName) ? "" : model.ProfileName;
        var reportGroupId = model.ReportGroupId;
        var search = GetSearchString(model);
        return base.SearchByFilterModel(model, data).Where(s =>
        s.Report.SubMenuID == reportGroupId
        && s.ProfileID == profile
        && (s.Report.Name).ToLower().Contains(search)
        ).Include(s => s.Report);
    }
}