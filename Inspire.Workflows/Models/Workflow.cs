using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Security.Models;

namespace Inspire.Workflows.Models
{
    public class Workflow:MakerChecker<int>
    {
        public string WorkflowTypeId { get; set; }
        public string SourceStageId { get; set; }
        public string DestinationStageId { get; set; }
        public string RecordActionId{get; set;}
        public WorkflowType WorkflowType { get; set; }
        public RecordAction RecordAction { get; set; }
        public WorkflowStage SourceStage { get; set; }
        public WorkflowStage DestinationStage { get; set; }
    }
    public class WorkflowDto : MakerCheckerDto<int>
    {
        public string WorkflowTypeId { get; set; }
        public string SourceStageId { get; set; }
        public string DestinationStageId { get; set; }
        public string RecordActionId { get; set; }
        
    }
}
