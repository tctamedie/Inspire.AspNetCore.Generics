
namespace Inspire.Modeller
{
    public interface IMaker<T>: IRecord<T>
        where T: IEquatable<T>
    {
        public DateTime DateCreated { get; set; }
        [StringLength(60)]
        public string CreatedBy { get; set; }
        
    }

    public interface IMakerDto<T>: IRecordDto<T>
        where T: IEquatable<T>
    {
    }
}
