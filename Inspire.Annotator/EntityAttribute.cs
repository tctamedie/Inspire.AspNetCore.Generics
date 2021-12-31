namespace Inspire.Annotator.Annotations
{
    public class EntityAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">identifier of the entity</param>
        /// <param name="displayName">label for the entity</param>
        /// <param name="order"> relative horizontal position</param>
        /// <param name="isKey"></param>
        /// <param name="width">bootstrap grid system's number of columns</param>
        public EntityAttribute([CallerMemberName] string id = "", string displayName = "", int order = 1, bool isKey = false, int? width=6)
        {
            Id = id.FirstLetterToLower();
            Order = order;
            IsKey = isKey;
            if (string.IsNullOrEmpty(displayName))
            {
                DisplayName = id.CamelSplit();
            }
            else
                DisplayName = displayName;
            Width = width;
            EntityId = id;
            
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
