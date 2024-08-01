
using Inspire.Annotations;

namespace Inspire.Modeller
{
    
    [Button(ButtonType.Edit)]
    [Button(ButtonType.Delete)]
    [Button(ButtonType.View)]
    public class Record<T>: IRecord<T>
        where T: IEquatable<T>
    {
        [TableColumn(order:1)]
        public virtual T Id { get; set; }
    }
    public class RecordDto<T>: IRecordDto<T>
        where T : IEquatable<T>
    {
        [Field(1,1, isKey:true)]
        public virtual T Id { get; set; }
    }
}
