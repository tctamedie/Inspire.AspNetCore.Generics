


using System.ComponentModel;

namespace Inspire.Modeller
{
    
    
    public class FilterModel
    {
        [TableFilter(1, 1, Width: 3)]
        public virtual string Search { get; set; }
        [TableFilter(2, 1, Name: "Status", DefaultValue: "U", Width: 3)]
        [List(Action: "GetStatus")]
        public virtual string AuthStatus { get; set; }
        [TableFilter(3, 1, DefaultValue: 50, ControlType: ControlType.Hidden)]
        public virtual int? Limit { get; set; } = null;
        [TableFilter(4, 1, ControlType: ControlType.Hidden)]
        public string Username { get; set; }
        [TableFilter(3, 1, DefaultValue: "8", ControlType: ControlType.Hidden)]
        public string AccessRight { get; set; }
    }
    [Serializable]
    [Button(ButtonType.Edit, Order: 1, Icon: "fa fa-edit", Class: "btn btn-default btn-xs", Action: "Edit", Title: "Edit")]
    [Button(ButtonType.View, Order: 4, Icon: "fa fa-external-link", Class: "btn btn-primary btn-xs", Action: "View", Title: "View")]
    [Button(ButtonType.Delete, Order: 2, Icon: "fa fa-trash", Class: "btn btn-default btn-xs btn-red", Action: "Delete", Title: "Delete")]
    [Button(ButtonType.Approve, Order: 3, Icon: "fa fa-check", Class: "btn btn-default btn-xs", Action: "Approve", Title: "Approve")]
    public abstract class MakerChecker<T> :Maker<T>, IMakerChecker<T> where T : IEquatable<T>
    {
        [TableColumn(isKey: true, displayName: "ID", order: 1, IsVisible: false)]
        public virtual T Id { get; set; }
        [StringLength(50)]
        public virtual string CapturedBy { get; set; }
        [StringLength(50)]
        public string AuthorisedBy { get; set; }
        public DateTime? DateCaptured { get; set; }
        [DefaultValue(typeof(DateTime), "01-April-2017")]
        public DateTime DateCreated { get; set; }
        public DateTime? DateAuthorised { get; set; }
        [StringLength(2)]
        public string AuthStatus { get; set; }
        public int AuthCount { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }
    public interface ITableView<T> : IMakerChecker<T> where T : IEquatable<T>
    {

    }
    public interface IMakerChecker<T>: IMaker<T>  where T : IEquatable<T>
    {
        public string UpdatedBy { get; set; }
        [StringLength(50)]
        public string AuthorisedBy { get; set; }
        public DateTime? DateCaptured { get; set; }
        public DateTime? DateAuthorised { get; set; }
        [StringLength(2)]
        public string AuthStatus { get; set; }
        public int AuthCount { get; set; }
    }
    public class MakerChecker
    {

        [StringLength(50)]
        public string CapturedBy { get; set; }
        [StringLength(50)]
        public string AuthorisedBy { get; set; }
        public DateTime? DateCaptured { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateAuthorised { get; set; }
        [StringLength(2)]
        public string AuthStatus { get; set; }
        public int AuthCount { get; set; }
    }

    public interface IMakerCheckerDto<T>: IMakerDto<T> where T : IEquatable<T>
    {
        [Field(1, 1, displayName: "ID", isKey: true)]
        public T Id { get; set; }
        [Field(0, 0, controlType: ControlType.Hidden, defaultValue: "Create")]
        public string UserAction { get; set; }

    }
    public abstract class MakerCheckerDto<T> : MakerDto<T>, IMakerCheckerDto<T> where T : IEquatable<T>
    {
        [Field(1, 1, displayName: "ID", isKey: true)]
        public virtual T Id { get; set; }
        [Field(0, 0, controlType: ControlType.Hidden, defaultValue: "Create")]
        public virtual string UserAction { get; set; }

    }
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException(string message) : base(message)
        {
        }
    }
    public class ValidationWarningException : Exception
    {
        public ValidationWarningException(string message) : base(message)
        {
        }
    }
    public class AccessException : Exception
    {
        public AccessException(string message) : base(message)
        {
        }
    }
}
