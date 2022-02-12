[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class ButtonAttribute: Attribute
{
     (string icon, string action, string title, string text, string _class) GetButtonDetails(ButtonType buttonType,string Icon= "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        switch (buttonType)
        {
            case ButtonType.Create:
                return GetCreateButtonDetails();                
            case ButtonType.Edit:
                return GetEditButtonDetails();
            case ButtonType.Delete:
                return GetDeleteButtonDetails();
            case ButtonType.Approve:
                return GetApproveButtonDetails();
            case ButtonType.View:
                return GetViewButtonDetails();
            default:
                return GetRejectButtonDetails();
                    
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
    public virtual (string icon, string action, string title, string text, string _class) GetDeleteButtonDetails(string Icon = "", string Action = "", string Title = "", string Text = "", string Class = "")
    {
        return (
            Assert(Icon, "delete"),
            Assert(Action, "Delete"),
            Assert(Title, "Delete"),
            Assert(Text, "Delete"),
            Assert(Class, "default btn-xs btn-red"));
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
    public ButtonAttribute(ButtonType buttonType, string Icon="", string Action="",string Title="", string Text= "", string Class= "")
    {
        (string icon, string action, string title, string text, string _class) = GetButtonDetails(buttonType);        

        this.Icon = icon;
        this.Action = action;
        this.Title = title;
        this.Text = text;
        this.Class = _class;
        ButtonType = buttonType;
    }
    public string Icon { get; }
    public string Action { get; }
    public string Title { get;  }
    public string Text { get;  }
    public string Class { get;  }
    public ButtonType ButtonType { get; set; }
}