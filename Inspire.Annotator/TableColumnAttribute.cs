namespace Inspire.Annotations
{
    public class TableColumnAttribute : EntityAttribute
    {
        public TableColumnAttribute([CallerMemberName] string id = "", string displayName = "", int order = 1, bool isKey = false, int width = 0, bool IsVisible = true) : base(id, displayName, order, isKey, width: width)
        {
            this.IsVisible = IsVisible;
        }
        public bool IsVisible { get; }
    }
}
