namespace Inspire.Workflows.Models
{
    [EntityConfiguration("WorkflowTypes", "SystemSecurity")]
    public class WorkflowType : Standard<string>
    {
    }
    [FormConfiguration("WorkflowTypes", "SystemSecurity")]
    public class WorkflowTypeDto : StandardDto<string>
    {
    }
}
