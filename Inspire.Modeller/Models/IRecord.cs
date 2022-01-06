namespace Inspire.Modeller
{
    public interface IRecord<T>
        where T: IEquatable<T>
    {
        public  T Id { get; set; }
    }
    public interface IRecordDto<T>
        where T : IEquatable<T>
    {
        public T Id { get; set; }
    }
}
