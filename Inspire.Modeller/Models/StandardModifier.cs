namespace Inspire.Modeller
{
    public class StandardModifier<T>: Modifier<T>, IStandardModifier<T>
        where T: IEquatable<T>
    {
        [TableColumn(order:2)]
        public virtual string Name { get; set; }
        
    }
    public class StandardModifierDto<T>: ModifierDto<T>,IStandardModifierDto<T>
        where T: IEquatable<T>
    {
        [Field(1,2)]
        public virtual string Name { get; set; }
    }
    public interface IStandardModifier<T> : IModifier<T>
        where T : IEquatable<T>
    {
        [TableColumn(order: 2)]
        public string Name { get; set; }

    }
    public interface IStandardModifierDto<T> : IModifierDto<T>
        where T : IEquatable<T>
    {
        [Field(1, 2)]
        public string Name { get; set; }
    }
}
