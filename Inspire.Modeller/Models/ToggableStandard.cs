namespace Inspire.Modeller
{
    public class ToggableStandard<T> : ToggableMakerChecker<T>, IToggableStandard<T> where T : IEquatable<T>
    {

        [TableColumn(order: 2)]
        public virtual string Name { get; set; }
    }
    public interface IToggableStandard<T> : IToggableMakerChecker<T>, IStandard<T> where T : IEquatable<T>
    {

    }
    public class ToggableStandardDto<T> : ToggableMakerCheckerDto<T>, IToggableStandardDto<T> where T : IEquatable<T>
    {
        [Field(2, 1)]
        [RequiredField]
        public virtual string Name { get; set; }
    }
    public interface IToggableStandardDto<T> : IStandardDto<T>, IToggableMakerCheckerDto<T> where T : IEquatable<T>
    {

    }
}
