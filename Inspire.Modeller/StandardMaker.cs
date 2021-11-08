using Inspire.Annotator.Annotations;
using System;

namespace Inspire.Modeller
{
    public class StandardMaker<T>: Maker<T>
        where T: IEquatable<T>
    {
        [Column(order:2)]
        public virtual string Name { get; set; }
        
    }
    public class StandardMakerDto<T>: MakerDto<T>
        where T: IEquatable<T>
    {
        [Field(1,2)]
        public virtual string Name { get; set; }
    }
}
