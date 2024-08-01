[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class ToggleButtonAttribute : Attribute
{

    public ToggleButtonAttribute(string CheckColumn = "IsActive", string TextWhenTrue = "Deactivate", string TextWhenFalse = "Activate", string IconWhenFalse = "fa fa-toggle-on", string IconWhenTrue = "fa fa-toggle-off", string Action = "Toggle", string Title = "Toggle", string Text = "Toggle", string Class = "btn-success")
    {
        this.TextWhenTrue = TextWhenTrue;
        this.TextWhenFalse = TextWhenFalse;
        this.CheckColumn = CheckColumn.FirstLetterToLower();
        this.IconWhenFalse = IconWhenFalse;
        this.IconWhenTrue = IconWhenTrue;
        this.Action = Action;
        this.Title = Title;
        this.Text = Text;
        this.Class = Class;
        ButtonType = ButtonType.Edit;

    }
    public string TextWhenTrue { get; }
    public string TextWhenFalse { get; }
    public string CheckColumn { get; }
    public string IconWhenFalse { get; }
    public string IconWhenTrue { get; }
    public string Action { get; }
    public string Title { get; }
    public string Text { get; }
    public string Class { get; }
    public ButtonType ButtonType { get; set; }

}
