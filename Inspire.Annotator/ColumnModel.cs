namespace Inspire.Annotations
{
    public class ColumnModel : EntityModel
    {
        public string Alignment { get; set; }
        public bool IsVisible { get; set; }
        public ColumnModel(int order, int? width, string id, bool isKey, string displayName, string entityId, bool isVisible, object defaultValue) : base(order, width, id, isKey, displayName, entityId, defaultValue)
        {
            IsVisible = isVisible;
        }
    }
}
