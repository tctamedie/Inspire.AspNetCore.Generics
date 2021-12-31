namespace Inspire.Annotator.Annotations
{
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TabAttribute : Attribute
    {
        public TabAttribute(int Order, string ID, string Name, bool IsActiveTab = false, [CallerMemberName] string Field = "all", bool IsHidden = false)
        {
            this.ID = ID.FirstLetterToLower();
            this.Name = Name;
            this.Order = Order;
            this.IsActiveTab = IsActiveTab;
            this.Field = Field.FirstLetterToLower();
            this.IsHidden = IsHidden;
        }
        public int Order { get; }
        public string ID { get; }
        public string Name { get; }
        public string Field { get; }
        public int Width { get; }
        public bool IsActiveTab { get; }
        public bool IsHidden { get; }

    }
}
