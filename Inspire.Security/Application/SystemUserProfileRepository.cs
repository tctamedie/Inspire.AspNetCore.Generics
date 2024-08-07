namespace Inspire.Security.Application;

public class UserFilterModel : RecordStatusFilter
{
    [TableFilter(1, ControlType: ControlType.Hidden)]
    public string UserName { get; set; }
}
public interface ISystemUserProfileRepository : IToggableService<SystemUserProfile, SystemUserProfileDto, UserFilterModel, string>
{

}
