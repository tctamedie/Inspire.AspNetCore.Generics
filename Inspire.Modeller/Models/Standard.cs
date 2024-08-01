namespace Inspire.Modeller
{
    public class Standard<T>: Record<T>, IStandard<T>
        where T: IEquatable<T>
    {
        [TableColumn(order:2)]
        public virtual string Name { get; set; }
        
    }
    public class StandardDto<T>: RecordDto<T>, IStandardDto<T>
        where T: IEquatable<T>
    {
        [Field(1,2)]
        public virtual string Name { get; set; }
    }
    public interface IStandard<T> : IRecord<T>
        where T : IEquatable<T>
    {
        [TableColumn(order: 2)]
        public string Name { get; set; }

    }
    public interface IStandardDto<T> : IRecordDto<T>
        where T : IEquatable<T>
    {
        [Field(1, 2)]
        public string Name { get; set; }
    }
}
