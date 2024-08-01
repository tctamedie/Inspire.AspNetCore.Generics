namespace Inspire.Modeller
{
    [ToggleButton]
    public class ToggableMakerChecker<T> : MakerChecker<T>, IToggableMakerChecker<T> where T : IEquatable<T>
    {
        [TableColumn(order: 50, displayName: "Active", width: 80)]
        public virtual string Active { get => IsActive == true ? "Yes" : "No"; }
        public virtual bool? IsActive { get; set; }
    }
    public interface IToggableMakerChecker<T> : IMakerChecker<T> where T : IEquatable<T>
    {
        [TableColumn(order: 50, displayName: "Active")]
        public virtual string Active { get => IsActive == true ? "Yes" : "No"; }
        public bool? IsActive { get; set; }
    }
    public class ToggableMakerCheckerDto<T> : MakerCheckerDto<T>, IToggableMakerCheckerDto<T> where T : IEquatable<T>
    {
        [Field(0, 0, controlType: ControlType.Hidden, defaultValue: "true")]
        public virtual bool? IsActive { get; set; }
    }
    public interface IToggableMakerCheckerDto<T> : IMakerCheckerDto<T> where T : IEquatable<T>
    {
        [Field(0, 0, controlType: ControlType.Hidden)]
        public bool? IsActive { get; set; }
    }
}
