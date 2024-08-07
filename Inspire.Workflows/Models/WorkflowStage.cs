

namespace Inspire.Workflows.Models
{
    [EntityConfiguration("WorkflowStages", "SystemSecurity")]
    public class WorkflowStage: Standard<int>
    {
        public string WorkflowTypeId { get; set; }
        public WorkflowType WorkflowType { get; set; }
    }
    [FormConfiguration("WorkflowStages", "SystemSecurity")]
    public class WorkflowStageDto : StandardDto<int>
    {
        public string WorkflowTypeId { get; set; }
        public WorkflowType WorkflowType { get; set; }
    }
}
