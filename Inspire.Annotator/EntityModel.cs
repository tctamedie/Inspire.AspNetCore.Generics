namespace Inspire.Annotations
{
    public class EntityModel
    {
        public EntityModel(int order, int? width, string id, bool isKey, string displayName, string entityId, object defaultValue)
        {
            Id = id;
            Width = width;
            Order = order;
            IsKey = isKey;
            DisplayName = displayName;
            EntityId = entityId;
            DefaultValue = defaultValue;

        }
        public int Order { get; set; }
        public int? Width { get; }
        public string Id { get; }
        public bool IsKey { get; }
        public string DisplayName { get; }
        public string DataType { get; set; }
        public string EntityId { get; }
        public object DefaultValue { get; set; }

    }
}
