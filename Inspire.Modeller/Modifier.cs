namespace Inspire.Modeller
{
    public class Modifier<T>: Maker<T>
        where T: IEquatable<T>
    {
        public virtual DateTime? DateModified { get; set; }
        [StringLength(60)]
        public virtual string ModifiedBy { get; set; }
    }
    public class ModifierDto<T> : MakerDto<T>
        where T : IEquatable<T>
    {
    }
}
