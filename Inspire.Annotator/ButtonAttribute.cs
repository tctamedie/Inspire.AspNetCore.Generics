[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class ButtonAttribute : Attribute
{
    protected virtual (string icon, string action, string title, string text, string _class) GetButtonDetails(ButtonType buttonType, string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        switch (buttonType)
        {
            case ButtonType.Create:
                return GetCreateButtonDetails(Icon, Action, Title, Text, Class);
            case ButtonType.Edit:
                return GetEditButtonDetails(Icon, Action, Title, Text, Class);
            case ButtonType.Delete:
                return GetDeleteButtonDetails(Icon, Action, Title, Text, Class);
            case ButtonType.Approve:
                return GetApproveButtonDetails(Icon, Action, Title, Text, Class);
            case ButtonType.View:
                return GetViewButtonDetails(Icon, Action, Title, Text, Class);
            case ButtonType.Download:
                return GetReportViewDetails(Icon, Action, Title, Text, Class);
            case ButtonType.Repay:
                return GetRepaymentButtonDetails(Icon, Action, Title, Text, Class);
            case ButtonType.Restructure:
                return GetRestructureButtonDetails(Icon, Action, Title, Text, Class);
            case ButtonType.Promote:
                return GetPromotionDetails(Icon, Action, Title, Text, Class);
            case ButtonType.Transfer:
                return GetTransferDetails(Icon, Action, Title, Text, Class);
            case ButtonType.Demote:
                return GetDemoteDetails(Icon, Action, Title, Text, Class);
            case ButtonType.Process:
                return GetProcessButtonDetails(Icon, Action, Title, Text, Class);
            default:
                return GetDefaultButtonDetails(Icon, Action, Title, Text, Class);

        }

    }
    public virtual (string icon, string action, string title, string text, string _class) GetCreateButtonDetails(string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        return ("add", "Create", "Add", "Add New", "primary btn-xs");
    }
    public virtual (string icon, string action, string title, string text, string _class) GetEditButtonDetails(string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        return (
            Assert(Icon, "edit"),
            Assert(Action, "Edit"),
            Assert(Title, "Edit"),
            Assert(Text, "Save Changes"),
            Assert(Class, "default btn-xs"));
    }
    public virtual (string icon, string action, string title, string text, string _class) GetProcessButtonDetails(string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        return (
            Assert(Icon, "cogs"),
            Assert(Action, "Process"),
            Assert(Title, "Reprocess"),
            Assert(Text, "Process"),
            Assert(Class, "default btn-xs btn-warning"));
    }
    public virtual (string icon, string action, string title, string text, string _class) GetDeleteButtonDetails(string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        return (
            Assert(Icon, "delete"),
            Assert(Action, "Delete"),
            Assert(Title, "Delete"),
            Assert(Text, "Delete"),
            Assert(Class, "default btn-xs btn-red"));
    }
    public virtual (string icon, string action, string title, string text, string _class) GetRepaymentButtonDetails(string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        return (
            Assert(Icon, "fa fa-paypal"),
            Assert(Action, "Repay"),
            Assert(Title, "Repay"),
            Assert(Text, "Repay"),
            Assert(Class, "default btn-xs btn-warning"));
    }
    public virtual (string icon, string action, string title, string text, string _class) GetRestructureButtonDetails(string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        return (
            Assert(Icon, "fa fa-adjust"),
            Assert(Action, "Restructure"),
            Assert(Title, "Restructure Loan"),
            Assert(Text, "Restructure Loan"),
            Assert(Class, "default btn-xs btn-success"));
    }
    public virtual (string icon, string action, string title, string text, string _class) GetPromotionDetails(string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        return (
            Assert(Icon, "fa fa-arrow-up"),
            Assert(Action, "Promote"),
            Assert(Title, "Promote"),
            Assert(Text, "Promote Employee"),
            Assert(Class, "default btn-xs btn-success"));
    }
    public virtual (string icon, string action, string title, string text, string _class) GetTransferDetails(string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        return (
            Assert(Icon, "fa fa-exchange"),
            Assert(Action, "Transfer"),
            Assert(Title, "Transfer"),
            Assert(Text, "Transfer Employee"),
            Assert(Class, "default btn-xs btn-warning"));
    }
    public virtual (string icon, string action, string title, string text, string _class) GetDemoteDetails(string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        return (
            Assert(Icon, "fa fa-arrow-down"),
            Assert(Action, "Demote"),
            Assert(Title, "Demote"),
            Assert(Text, "Demote Employee"),
            Assert(Class, "default btn-xs btn-danger"));
    }
    public virtual (string icon, string action, string title, string text, string _class) GetApproveButtonDetails(string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        return (
            Assert(Icon, "check"),
            Assert(Action, "Approve"),
            Assert(Title, "Approve"),
            Assert(Text, "Approve"),
            Assert(Class, "default btn-xs btn-success"));
    }
    public virtual (string icon, string action, string title, string text, string _class) GetRejectButtonDetails(string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        return (
            Assert(Icon, "check"),
            Assert(Action, "Reject"),
            Assert(Title, "Reject"),
            Assert(Text, "Reject"),
            Assert(Class, "default btn-xs btn-red"));
    }
    public virtual (string icon, string action, string title, string text, string _class) GetDefaultButtonDetails(string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        return (
            Icon,
            Action,
            Title,
            Text,
            Class);
    }
    public virtual (string icon, string action, string title, string text, string _class) GetReportViewDetails(string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        return (
            Assert(Icon, "download"),
            Assert(Action, "DownloadFile"),
            Assert(Title, "Download"),
            Assert(Text, "Download"),
            Assert(Class, "default btn-xs btn-success"));
    }
    public virtual (string icon, string action, string title, string text, string _class) GetViewButtonDetails(string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        return (
            Assert(Icon, "external-link"),
            Assert(Action, "View"),
            Assert(Title, "View"),
            Assert(Text, ""),
            Assert(Class, "primary btn-xs"));
    }
    string Assert(string val, string alt)
    {
        return string.IsNullOrEmpty(val) ? alt : val;
    }
    public ButtonAttribute(ButtonType buttonType, string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "", int Order=5)
    {
        (string icon, string action, string title, string text, string _class) = GetButtonDetails(buttonType, Icon, Action, Title, Text, Class);

        this.Icon = icon;
        this.Action = action;
        this.Title = title;
        this.Text = text;
        this.Class = _class;
        ButtonType = buttonType;
        this.Order = Order;
    }
    public string Icon { get; }
    public string Action { get; }
    public string Title { get; }
    public string Text { get; }
    public string Class { get; }
    public int Order { get; }
    public ButtonType ButtonType { get; set; }
}