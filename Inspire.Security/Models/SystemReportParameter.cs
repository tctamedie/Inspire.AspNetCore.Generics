using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Security.Models
{
    public class ReportFilterModel : RecordStatusFilter
    {
        [TableFilter(1, ControlType: ControlType.Hidden)]
        public string ReportId { get; set; }
    }

    [EntityConfiguration("Parameters", "SystemSecurity", "Parameters", "Parameter")]
    [BreadCrumb(1, "Reports", "SystemSecurity", "Reports", "ReportID")]
    public class SystemReportParameter : ToggableMakerChecker<int>
    {
        //[Column("ID")]
        [TableColumn(order: 1, isKey: true, width: 50)]
        public override int Id { get; set; }
        public string ReportID { get; set; }
        public string Prefix { get; set; }
        public bool IsOptional { get; set; }
        public string ParameterID { get; set; }
        [TableColumn(order: 2)]
        public string ParameterName { get => SysParameter == null ? "" : SysParameter.Name; }
        [TableColumn(order: 3)]
        public int Row { get; set; }
        [TableColumn(order: 4)]
        public int Column { get; set; }
        [ForeignKey("ReportID")]
        public virtual Report Report { get; set; }
        [ForeignKey("ParameterID")]
        public virtual SysParameter SysParameter { get; set; }
    }

    [FormConfiguration("Parameters", "SystemSecurity", "Parameters", "Parameter")]
    public class SystemReportParameterDto : ToggableMakerCheckerDto<int>
    {
        //[Column("ID")]
        [Field(0, 0, isKey: true, controlType: ControlType.Hidden)]
        public override int Id { get; set; }
        [Field(0, 0, controlType: ControlType.Hidden)]
        public string ReportID { get; set; }
        [Field(2, 1)]
        public string Prefix { get; set; }
        [Field(0, 0, controlType: ControlType.Hidden)]
        public bool IsOptional { get; set; }
        [Field(1, 1, displayName: "Parameter Group")]
        [List("ParameterGroups")]
        public string ParameterID { get; set; }
        [Field(1, 2)]
        public int Row { get; set; }
        [Field(2, 2)]
        public int Column { get; set; }

    }
}