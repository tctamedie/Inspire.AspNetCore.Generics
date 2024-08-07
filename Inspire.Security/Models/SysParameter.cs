namespace Inspire.Security.Models
{
    
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public enum ReportParameterDataType
    {
        Employee = 1,
        Edcode,
        Grade,
        LoanType,
        Date,
        Boolean,
        String,
        Number,
        Department,
        Section,
        Division,
        Branch,
        User,
        DatabaseTable

    }
    [EntityConfiguration("ParameterGroups", "SystemSecurity", "Parameter Groups", "Parameter Group")]
    public partial class SysParameter : StandardMakerChecker<string>
    {
        public SysParameter()
        {
            SystemReportParameters = new HashSet<SystemReportParameter>();
        }
        [StringLength(20)]
        //[Key
        //[Column("SysParameterCode")]
        [TableColumn(order: 1, isKey: true, width: 80)]
        public override string Id { get; set; }
        public ReportParameterDataType ReportParameterDataType { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
        public string FilterColumn { get; set; }
        public string FilterValue { get; set; }
        public string ValueField { get; set; }
        public string TextField { get; set; }
        public string SortField { get; set; }
        public bool MultipleSelect { get; set; }
        public string OnSelectedChange { get; set; }
        public string OnField { get; set; }
        public string ListAction { get; set; }
        public virtual ICollection<SystemReportParameter> SystemReportParameters { get; set; }
    }
    [FormConfiguration("ParameterGroups", "SystemSecurity", "Parameter Groups", "Parameter Group")]
    public partial class SysParameterDto : StandardMakerCheckerDto<string>
    {
        [Field(0, 1, isKey: true, controlType: ControlType.Hidden)]
        public override string Id { get; set; }
        [Field(1, 1)]
        public override string Name { get; set; }
        [Field(2, 1, displayName: "Data Type")]
        [List(Action: "GetDataTypes")]
        public ReportParameterDataType ReportParameterDataType { get; set; }
        [Field(1, 2)]
        public string Controller { get; set; }
        [Field(2, 2)]
        public string Area { get; set; }
        [Field(1, 3)]
        public string FilterColumn { get; set; }
        [Field(2, 3)]
        public string FilterValue { get; set; }
        [Field(1, 4)]
        public string ValueField { get; set; }
        [Field(2, 4)]
        public string TextField { get; set; }
        [Field(1, 5)]
        public string SortField { get; set; }
        [Field(2, 5)]
        [List(Action: "BooleanList")]
        public bool MultipleSelect { get; set; }
        [Field(1, 6, width: 12, displayName: "Function to Trigger when Value Changes")]
        public string OnSelectedChange { get; set; }
        [Field(1, 7, displayName: "Update Field")]
        public string OnField { get; set; }
        [Field(2, 7, displayName: "Action")]
        public string ListAction { get; set; }
    }
}
