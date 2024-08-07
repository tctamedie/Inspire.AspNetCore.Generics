
namespace Inspire.Security.Application;


public interface ISystemUserRepository : IStandardMakerCheckerService<SystemUser, SystemUserDto, string, StandardStatusFilter>
{
    string Folder { get; set; }
    
    Task Register(RegisterModelView model, string userId);
}
