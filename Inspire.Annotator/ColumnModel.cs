namespace Inspire.Annotator.Annotations
{
    public class ColumnModel : EntityModel
    {
        public string Alignment { get; set; }
        public ColumnModel(int order, int? width, string id, bool isKey, string displayName, string entityId) : base(order, width, id, isKey, displayName, entityId)
        {
        }
    }
}
