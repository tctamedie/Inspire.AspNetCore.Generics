using Inspire.Annotator.Annotations;
using System;

namespace Inspire.Modeller
{
    public class StandardMakerChecker<T>: MakerChecker<T>
        where T: IEquatable<T>
    {
        [Column(order:2)]
        public virtual string Name { get; set; }
        
    }
    public class StandardMakerCheckerDto<T>: MakerCheckerDto<T>
        where T: IEquatable<T>
    {
        [Field(1,2)]
        public virtual string Name { get; set; }
    }
}
