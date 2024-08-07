namespace Inspire.Modeller
{

    public class Maker<T>: Record<T>, IMaker<T>
        where T: IEquatable<T>
    {
        public virtual DateTime DateCreated { get; set; }
        [StringLength(60)]
        public virtual string CreatedBy { get; set; }
        
    }

    public class MakerDto<T>: RecordDto<T>, IMakerDto<T>
        where T: IEquatable<T>
    {
    }
    public interface IExcelUpload
    {
    }
    public class GenericData<T> 
    {
        public T ID { get; set; }
        public string Name { get; set; }
    }
    
}
