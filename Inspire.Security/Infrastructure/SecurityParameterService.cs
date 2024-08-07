namespace Inspire.Security.Infrastructure
{
    public class SecurityParameterService(SecurityContext context) : MakerCheckerService<SecurityOption, SecurityOptionDto, int, SecurityContext, RecordStatusFilter>(context), ISecurityParameterService
    {
        
    }

}
