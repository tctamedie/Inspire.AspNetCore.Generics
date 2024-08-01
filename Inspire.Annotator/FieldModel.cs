﻿namespace Inspire.Annotations
{
    public class FieldModel : EntityModel
    {
        public FieldModel(int row, int order, int? width, string id, bool isKey, string displayName, bool autogenerated, ControlType controlType, string tabId, string entityId, bool isRequired, object defaultValue, string placeholder ) : base(order, width, id, isKey, displayName, entityId,defaultValue)
        {
            Row = row;
            Autogenerated = autogenerated;
            ControlType = controlType.ToString().ToLower();
            TabId = tabId;
            IsRequired = isRequired;
            Hidden = controlType == Annotations.ControlType.Hidden;
            Placeholder = placeholder;
        }
        public bool Hidden { get; set; }
        public int Row { get; set; }
        public int? MinimumLength { get; set; }
        public int MaximumLength { get; set; }
        public bool Autogenerated { get; set; }
        public string TabId { get; }
        public string Placeholder { get; }
        public string ControlType { get; set; }
        public bool IsRequired { get; }
        public ListModel List { get; set; }
    }
}
