namespace Inspire.Modeller
{
    public class Modifier<T>: Maker<T>, IModifier<T>
        where T: IEquatable<T>
    {
        public virtual DateTime? DateModified { get; set; }
        [StringLength(60)]
        public virtual string ModifiedBy { get; set; }
    }
    public class ModifierDto<T> : MakerDto<T>, IModifierDto<T>
        where T : IEquatable<T>
    {
    }
    public interface IModifier<T> : IMaker<T>
        where T : IEquatable<T>
    {
        public DateTime? DateModified { get; set; }
        [StringLength(60)]
        public string ModifiedBy { get; set; }
    }
    public interface IModifierDto<T> : IMakerDto<T>
        where T : IEquatable<T>
    {
    }
}
