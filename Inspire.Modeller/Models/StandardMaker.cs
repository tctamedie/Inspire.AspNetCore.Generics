namespace Inspire.Modeller
{
    public class StandardMaker<T>: Maker<T>, IStandardMaker<T>
        where T: IEquatable<T>
    {
        [TableColumn(order:2)]
        public virtual string Name { get; set; }
        
    }
    public class StandardMakerDto<T>: MakerDto<T>, IStandardMakerDto<T>
        where T: IEquatable<T>
    {
        [Field(1,2)]
        public virtual string Name { get; set; }
    }
    public interface IStandardMaker<T> : IMaker<T>
        where T : IEquatable<T>
    {
        [TableColumn(order: 2)]
        public  string Name { get; set; }

    }
    public interface IStandardMakerDto<T> : IMakerDto<T>
        where T : IEquatable<T>
    {
        [Field(1, 2)]
        public string Name { get; set; }
    }
}
