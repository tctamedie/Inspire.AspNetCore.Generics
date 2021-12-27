namespace Inspire.Annotator.Annotations
{
    public class EntityModel
    {
        public EntityModel(int order, int? width, string id, bool isKey, string displayName, string entityId)
        {
            Id = id;
            Width = width;
            Order = order;
            IsKey = isKey;
            DisplayName = displayName;
            EntityId = entityId;
            
        }
        public int Order { get; }
        public int? Width { get; }
        public string Id { get; }
        public bool IsKey { get; }
        public string DisplayName { get; }
        public string DataType { get; set; }
        public string EntityId { get; }
        
    }
}
