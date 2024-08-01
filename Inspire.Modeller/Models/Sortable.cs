namespace Inspire.Modeller
{
    [SortButton(1)]
    [SortButton(2, Icon: "fa fa-arrow-down", Action: "MoveDown", Title: "Demote", Text: "Demote", Class: "btn-warning")]
    public class SortableMakerChecker<T> : MakerChecker<T>, ISortableMakerChecker<T> where T : IEquatable<T>
    {
        [TableColumn(isKey: true, order: 1, width: 80)]
        public override T Id { get; set; }
        [TableColumn(order: 50, width: 80)]
        public int Order { get; set; }
    }
    public interface ISortableMakerChecker<T> : IMakerChecker<T> where T : IEquatable<T>
    {
        [TableColumn(order: 50, width: 80)]
        public int Order { get; set; }
    }
    public class SortableMakerCheckerDto<T> : MakerCheckerDto<T>, ISortableMakerCheckerDto<T> where T : IEquatable<T>
    {
        [Field(0, 0, controlType: ControlType.Hidden)]
        public int Order { get; set; }
    }
    public interface ISortableMakerCheckerDto<T> : IMakerCheckerDto<T> where T : IEquatable<T>
    {
        [Field(0, 0, controlType: ControlType.Hidden)]
        public int Order { get; set; }
    }
}
