namespace Inspire.Annotations
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TabModel
    {
        public TabModel(int Order, string ID, string Name, bool IsActiveTab = false, [CallerMemberName] string Field = "all", bool IsHidden = false)
        {
            this.ID = ID.FirstLetterToLower();
            this.Name = Name;
            this.Order = Order;
            this.IsActiveTab = IsActiveTab;
            this.Field = Field.FirstLetterToLower();
            this.IsHidden = IsHidden;
            Rows = new List<TabRow>();
        }
        public int Order { get; }
        public string ID { get; }
        public string Name { get; }
        public string Field { get; }
        public int Width { get; }
        public bool IsActiveTab { get; }
        public bool IsHidden { get; }
        public List<TabRow> Rows { get; set; }

    }
    public class TabRow
    {
        public int Order { get; set; }
        public List<FieldModel> Fields { get; set; }
    }
}
