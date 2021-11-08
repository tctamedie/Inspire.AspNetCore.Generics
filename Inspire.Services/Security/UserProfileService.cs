using Inspire.DataAccess.Security;
using Inspire.Modeller;
using Inspire.Modeller.Models.Security;

namespace Inspire.Services.Security
{
    public interface IUserProfileService<TContext> : IStandardModifierCheckerService<UserProfile, UserProfileDto, string, TContext, StandardStatusFilter>
        where TContext: SecurityContext
    {
    }

    public class UserProfileService<TContext> : StandardModifierCheckerService<UserProfile, UserProfileDto, string, TContext, StandardStatusFilter>, IUserProfileService<TContext>
        where TContext : SecurityContext
    {
        public UserProfileService(TContext db) : base(db)
        {
        }
        

    }
}
