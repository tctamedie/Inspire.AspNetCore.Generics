using System;

namespace Inspire.Annotator
{
    using Annotations;
    [Button(ButtonType.Edit)]
    [Button(ButtonType.Delete)]
    [Button(ButtonType.View)]
    public class Record<T>
        where T: IEquatable<T>
    {
        [Column(order:1)]
        public virtual T Id { get; set; }
    }
    public class RecordDto<T>
        where T : IEquatable<T>
    {
        [Field(1,1, isKey:true)]
        public virtual T Id { get; set; }
    }
}
