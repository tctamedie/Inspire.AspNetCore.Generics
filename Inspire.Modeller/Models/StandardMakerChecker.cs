using Inspire.Annotations;

namespace Inspire.Modeller
{
    public class StandardMakerChecker<T>: MakerChecker<T>, IStandardMakerChecker<T>
        where T: IEquatable<T>
    {
        [TableColumn(order:2)]
        public virtual string Name { get; set; }
        
    }
    public class StandardMakerCheckerDto<T>: MakerCheckerDto<T>, IStandardMakerCheckerDto<T>
        where T: IEquatable<T>
    {
        [Field(1,2)]
        public virtual string Name { get; set; }
    }
    public interface IStandardMakerChecker<T> : IMakerChecker<T>
        where T : IEquatable<T>
    {
        
        public string Name { get; set; }

    }
    public interface IStandardMakerCheckerDto<T> : IMakerCheckerDto<T>
        where T : IEquatable<T>
    {
        [Field(1, 2)]
        public string Name { get; set; }
    }
}
