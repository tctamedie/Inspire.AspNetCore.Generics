using Inspire.Annotator;
using Inspire.Annotator.Annotations;
using System;

namespace Inspire.Modeller
{
    public class Standard<T>: Record<T>
        where T: IEquatable<T>
    {
        [Column(order:1)]
        public virtual string Name { get; set; }
        
    }
    public class StandardDto<T>: RecordDto<T>
        where T: IEquatable<T>
    {
        [Field(1,1)]
        public virtual string Name { get; set; }
    }
}
