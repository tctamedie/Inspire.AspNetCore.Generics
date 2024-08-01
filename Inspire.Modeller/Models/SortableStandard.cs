namespace Inspire.Modeller
{
    public class SortableStandard<T> : SortableMakerChecker<T>, ISortableStandard<T> where T : IEquatable<T>
    {

        [TableColumn(order: 2)]
        public virtual string Name { get; set; }
    }
    public interface ISortableStandard<T> : ISortableMakerChecker<T>, IStandard<T> where T : IEquatable<T>
    {

    }
    public class SortableStandardDto<T> : SortableMakerCheckerDto<T>, ISortableStandardDto<T> where T : IEquatable<T>
    {
        [Field(2, 1)]
        public virtual string Name { get; set; }
    }
    public interface ISortableStandardDto<T> : IStandardDto<T>, ISortableMakerCheckerDto<T> where T : IEquatable<T>
    {

    }
}
