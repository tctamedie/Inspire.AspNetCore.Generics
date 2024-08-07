namespace Inspire.Modeller
{
    [Button(ButtonType.Approve)]
    public class ModifierChecker<T>: Modifier<T>, IModifierChecker<T>
        where T: IEquatable<T>
    {
        public DateTime? DateAuthorised { get; set; }
        [StringLength(60)]
        public string AuthorisedBy { get; set; }
        [StringLength(2)]
        public string AuthStatus { get; set; }
        public int AuthCount { get; set; }
    }
    public class ModifierCheckerDto<T> : ModifierDto<T>, IModifierCheckerDto<T>
        where T : IEquatable<T>
    {
    }
    
    public interface IModifierChecker<T> : IModifier<T>
        where T : IEquatable<T>
    {
        public DateTime? DateAuthorised { get; set; }
        [StringLength(60)]
        public string AuthorisedBy { get; set; }
        [StringLength(2)]
        public string AuthStatus { get; set; }
        public int AuthCount { get; set; }
    }
    public interface IModifierCheckerDto<T> : IModifierDto<T>
        where T : IEquatable<T>
    {
    }
}
