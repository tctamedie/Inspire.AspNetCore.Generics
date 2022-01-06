namespace Inspire.Modeller
{
    [Button(ButtonType.Approve)]
    public class MakerChecker<T>: Maker<T>
        where T: IEquatable<T>
    {
        public virtual DateTime? DateAuthorised { get; set; }
        [StringLength(60)]
        public virtual string AuthorisedBy { get; set; }
        [StringLength(2)]
        public virtual string AuthStatus { get; set; }
        public virtual int AuthCount { get; set; }
    }
    public class MakerCheckerDto<T> : MakerDto<T>
        where T : IEquatable<T>
    {
    }
}
