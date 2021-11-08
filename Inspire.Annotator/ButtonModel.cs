using Inspire.Annotator.Annotations;

public class ButtonModel
{
    public ButtonModel(string icon, string action, string title, string text, string _class, ButtonType buttonType)
    {
        Icon = icon;
        Action = action;
        Title = title;
        Text = text;
        Class = _class;
        ButtonType = buttonType;
    }
    public string Icon { get; set; }
    public string Action { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public string Class { get; set; }
    public ButtonType ButtonType { get; set; }
}
