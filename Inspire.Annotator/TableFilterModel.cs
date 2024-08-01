namespace Inspire.Annotations
{
    public class TableFilterModel : EntityModel
    {
        public TableFilterModel(int row, int order, int? width, string id, string displayName, object defaultValue, ControlType controlType, string onChangeAction, string entityId) : base(order, width, id, false, displayName, entityId, defaultValue)
        {
            Row = row;
            ControlType = controlType.ToString().ToLower();
            OnChangeAction = onChangeAction;
            Hidden = controlType == Annotations.ControlType.Hidden;
        }
        public string ControlType { get; }
        
        public string OnChangeAction { get; }
        public bool Hidden { get; }
        public int Row { get; }
        public ListModel List { get; set; }
    }
}
