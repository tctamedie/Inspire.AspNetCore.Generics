public class ButtonModel
{
    public ButtonModel(string icon, string action, string title, string text, string _class, ButtonType buttonType, int order)
    {
        Icon = icon;
        Action = action;
        Title = title;
        Text = text;
        Class = _class;
        ButtonType = buttonType;
        Order = order;
    }
    public string Icon { get; set; }
    public string Action { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public string Class { get; set; }
    public int Order { get; set; }
    public ButtonType ButtonType { get; set; }
}
