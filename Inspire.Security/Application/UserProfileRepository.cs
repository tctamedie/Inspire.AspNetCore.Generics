namespace Inspire.Security.Application;

public interface IUserProfileRepository : IStandardMakerCheckerService<UserProfile, UserProfileDto, string, StandardStatusFilter>
{
    List<GenericData<string>> GetRights();
    Task<OutputHandler> CopyProfile(string ProfileName, string OldProfileName, string CopyAs, string capturedBy, int authorisers);
}
