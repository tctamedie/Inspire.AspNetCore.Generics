namespace Inspire.Annotator.Annotations
{
    public class TableFilterAttribute : EntityAttribute
    {
        /// <summary>
        /// Sets Filtering fields for entity's entries on the html table
        /// </summary>
        /// <param name="Order">Relative horizontal position of the field </param>
        /// <param name="Row">Relative veritcal position of the field</param>
        /// <param name="ID">the identifier for field</param>
        /// <param name="Name">the label for the field</param>
        /// <param name="Width"> bootstrap grid system's number of columns</param>
        /// <param name="ControlType"> type of the control be it html's hidden input etc</param>
        /// <param name="DefaultValue">default value for the input field</param>
        /// <param name="OnChangeAction">event that should be triggered if the value changes, if not specified it will look for Search javascript method</param>
        public TableFilterAttribute(int Order,int Row=1, [CallerMemberName] string ID = "", [CallerMemberName] string Name = "", int Width = 6,  ControlType ControlType = Annotations.ControlType.Text,  string DefaultValue="", string OnChangeAction="Search") : base(ID, Name, Order, false, Width)
        {
            this.ControlType = ControlType;
            this.DefaultValue = DefaultValue;
            this.OnChangeAction = OnChangeAction;
            this.Row = Row;
        }
       
        public ControlType ControlType { get; }
        public string DefaultValue { get;  }
        public string OnChangeAction { get; set; }
        public int Row { get; }

    }
}
