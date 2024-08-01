

using Inspire.Modeller.Security;

namespace Inspire.Modeller.Models.Security
{
    [EntityConfiguration("ProfileWorkflows", "SystemSecurity", "Profile Workflows", "Profile Workflow")]
    [BreadCrumb(1, "UserProfiles", "SystemSecurity", "User Profiles", "ProfileName")]
    public class UserProfileWorkflowRight : ToggableMakerChecker<int>
    {
        //[Key
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [TableColumn(order: 1, width: 50, isKey: true)]
        public override int Id { get; set; }

        public string ProfileName { get; set; }
        public int MenuID { get; set; }
        [TableColumn(order: 2)]
        public string Menu { get => SubMenu == null ? "" : SubMenu.Name; }
        public string WorkflowStageId { get; set; }
        [TableColumn(order: 3, displayName: "Profile Rights")]
        public string WorkflowDescription
        {
            get
            {
                return WorkflowStage==null?"":WorkflowStage.Name;
            }
        }
        [ForeignKey("ProfileName")]
        public virtual UserProfile UserProfile { get; set; }
        [ForeignKey("MenuID")]
        public virtual SubMenu SubMenu { get; set; }
        public WorkflowStage WorkflowStage { get; set; }
        public int Authorisers { get; set; }
    }
    [FormConfiguration("ProfileWorkflows", "SystemSecurity", "Profile Workflows", "Profile Workflow")]
    public class UserProfileWorkflowRightDto : ToggableMakerCheckerDto<int>
    {
        [Field(0, 0, controlType: ControlType.Hidden, isKey: true)]
        public override int Id { get; set; }
        [Field(0, 0, controlType: ControlType.Hidden)]
        public string ProfileName { get; set; }
        [Field(1, 1, displayName: "Menu", width: 12)]
        [List(Action: "GetMenus", OnSelectChange: "LoadRights")]
        public int MenuID { get; set; }
        [Field(1, 2, displayName: "Rights", width: 12)]
        [List(Action: "GetRights")]
        public string WorkflowStageId { get; set; }
        [Field(1, 3, displayName: "Number of Authorisers", width: 12)]
        public int Authorisers { get; set; }

    }
}
