namespace Inspire.Modeller
{
    public class Standard<T>: Record<T>
        where T: IEquatable<T>
    {
        [Column(order:2)]
        public virtual string Name { get; set; }
        
    }
    public class StandardDto<T>: RecordDto<T>
        where T: IEquatable<T>
    {
        [Field(1,2)]
        public virtual string Name { get; set; }
    }
}
