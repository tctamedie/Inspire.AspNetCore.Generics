namespace Inspire.Modeller
{
    public class StandardModifierChecker<T>: ModifierChecker<T>
        where T: IEquatable<T>
    {
        public virtual string Name { get; set; }
        
    }
    public class StandardModifierCheckerDto<T>: ModifierCheckerDto<T>
        where T: IEquatable<T>
    {
        public virtual string Name { get; set; }
    }
}
