[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class SortButtonAttribute : Attribute
{

    public SortButtonAttribute(int Order, string CheckColumn = "Order", string Icon = "fa fa-arrow-up", string Action = "MoveUp", string Title = "Promote", string Text = "Promote", string Class = "btn-success")
    {
        this.Order = Order;
        this.Icon = Icon;
        this.CheckColumn = CheckColumn.FirstLetterToLower();
        this.Action = Action;
        this.Title = Title;
        this.Text = Text;
        this.Class = Class;
        ButtonType = ButtonType.Edit;

    }
    public int Order { get; }
    public string Action { get; }
    public string CheckColumn { get; }
    public string Icon { get; }
    public string Title { get; }
    public string Text { get; }
    public string Class { get; }
    public ButtonType ButtonType { get; set; }

}
public class SortButtonModel
{

    public SortButtonModel(int Order, string CheckColumn, string Icon, string Action, string Title, string Text, string Class)
    {
        this.Order = Order;
        this.Icon = Icon;
        this.CheckColumn = CheckColumn.FirstLetterToLower();
        this.Action = Action;
        this.Title = Title;
        this.Text = Text;
        this.Class = Class;
        ButtonType = ButtonType.Edit;

    }
    public int Order { get; }
    public string Action { get; }
    public string CheckColumn { get; }
    public string Icon { get; }
    public string Title { get; }
    public string Text { get; }
    public string Class { get; }
    public ButtonType ButtonType { get; set; }

}