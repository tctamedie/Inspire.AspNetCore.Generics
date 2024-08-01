using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Modeller.Security
{
    public class WorkflowStage: Standard<string>
    {
        public string WorkflowTypeId { get; set; }
        public WorkflowType WorkflowType { get; set; }
    }
    public class WorkflowStageDto : StandardDto<string>
    {
        public string WorkflowTypeId { get; set; }
        public WorkflowType WorkflowType { get; set; }
    }
}
