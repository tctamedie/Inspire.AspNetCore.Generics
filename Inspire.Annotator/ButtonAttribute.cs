[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class ButtonAttribute: Attribute
{
     (string icon, string action, string title, string text, string _class) GetButtonDetails(ButtonType buttonType)
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
    public virtual (string icon, string action, string title, string text, string _class) GetCreateButtonDetails()
    {
        return ("add", "Create", "Add", "Add New", "primary btn-xs");
    }
    public virtual (string icon, string action, string title, string text, string _class) GetEditButtonDetails()
    {
        return ("edit", "Edit", "Edit", "Save Changes", "primary btn-xs");
    }
    public virtual (string icon, string action, string title, string text, string _class) GetDeleteButtonDetails()
    {
        return ("trash", "Delete", "Delete", "Delete", "primary btn-xs btn-red");
    }
    public virtual (string icon, string action, string title, string text, string _class) GetApproveButtonDetails()
    {
        return ("check", "Approve", "Approve", "Approve", "primary btn-xs btn-success");
    }
    public virtual (string icon, string action, string title, string text, string _class) GetRejectButtonDetails()
    {
        return ("check", "Reject", "Reject", "Reject", "primary btn-xs btn-red");
    }
    public virtual (string icon, string action, string title, string text, string _class) GetViewButtonDetails()
    {
        return ("external-link", "View", "View", "", "primary btn-xs");
    }
    public ButtonAttribute(ButtonType buttonType)
    {
        (string icon, string action, string title, string text, string _class) = GetButtonDetails(buttonType);        

        Icon = icon;
        Action = action;
        Title = title;
        Text = text;
        Class = _class;
        ButtonType = buttonType;
    }
    public string Icon { get; }
    public string Action { get; }
    public string Title { get;  }
    public string Text { get;  }
    public string Class { get;  }
    public ButtonType ButtonType { get; set; }
}