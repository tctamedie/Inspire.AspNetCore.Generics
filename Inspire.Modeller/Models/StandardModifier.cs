﻿namespace Inspire.Modeller
{
    public class StandardModifier<T>: Modifier<T>
        where T: IEquatable<T>
    {
        [TableColumn(order:2)]
        public virtual string Name { get; set; }
        
    }
    public class StandardModifierDto<T>: ModifierDto<T>
        where T: IEquatable<T>
    {
        [Field(1,2)]
        public virtual string Name { get; set; }
    }
}
