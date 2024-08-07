namespace Inspire.Modeller
{
    public class StandardModifierChecker<T>: ModifierChecker<T>, IStandardModifierChecker<T>
        where T: IEquatable<T>
    {
        public virtual string Name { get; set; }
        
    }
    public class StandardModifierCheckerDto<T>: ModifierCheckerDto<T>, IStandardModifierCheckerDto<T>
        where T: IEquatable<T>
    {
        public virtual string Name { get; set; }
    }
    public interface IStandardModifierChecker<T> : IModifierChecker<T>
        where T : IEquatable<T>
    {
        public  string Name { get; set; }

    }
    public interface IStandardModifierCheckerDto<T> : IModifierCheckerDto<T>
        where T : IEquatable<T>
    {
        public string Name { get; set; }
    }
}
