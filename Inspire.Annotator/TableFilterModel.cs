namespace Inspire.Annotator.Annotations
{
    public class TableFilterModel : EntityModel
    {
        public TableFilterModel(int row,int order, int? width, string id, string displayName, string defaultValue, ControlType controlType, string onChangeAction, string entityId) : base(order, width, id, false, displayName, entityId)
        {
            Row = row;
            ControlType = controlType.ToString().ToLower();
            OnChangeAction = onChangeAction;
            DefaultValue = defaultValue;
            Hidden = controlType == Annotations.ControlType.Hidden;
        }
        public string ControlType { get; }
        public string DefaultValue { get; set; }
        public string OnChangeAction { get; }
        public bool Hidden { get;  }
        public int Row { get; }
        public ListModel List { get; set; }
    }
}
