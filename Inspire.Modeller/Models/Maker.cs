namespace Inspire.Modeller
{

    public class Maker<T>: Record<T>
        where T: IEquatable<T>
    {
        public virtual DateTime DateCreated { get; set; }
        [StringLength(60)]
        public virtual string CreatedBy { get; set; }
        
    }

    public class MakerDto<T>: RecordDto<T>
        where T: IEquatable<T>
    {
    }
    public interface IExcelUpload
    {
    }
    public class GenericData<T> where T: IEquatable<T>
    {

    }
}
