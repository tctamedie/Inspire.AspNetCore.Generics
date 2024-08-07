﻿public class LinkAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Controller">The controller to be called when the link is clicked</param>
    /// <param name="LinkButtonName">The Name of the Link Button. By Default it take the name of the Navigation Collection Item Name prepended with btn</param>
    /// <param name="LinkButtonTitle">The Title displayed when the Link Button is hovered over</param>
    /// <param name="LinkButtonIcon">The Icon that will be diplayed</param>
    /// <param name="LinkButtonClass">The Class for the link button</param>
    /// <param name="Action">The Action to be executed on the given controller</param>
    /// <param name="ID">the id of the Collection Item</param>
    /// <param name="Area">The area under which the controller falls</param>
    public LinkAttribute(string Controller, int Order = 1, string LinkButtonName = "", string LinkButtonTitle = "", string LinkButtonIcon = "fa fa-external-link", string LinkButtonClass = "btn btn-primary btn-xs", string Action = "Index", [CallerMemberName] string ID = "", string Area = "", LinkType linkType = LinkType.HyperLink, ButtonType buttonType = ButtonType.Edit)
    {
        this.ID = ID.FirstLetterToLower();
        this.Controller = Controller;
        this.Action = Action;
        this.Area = Area;
        if (string.IsNullOrEmpty(LinkButtonName))
            this.LinkButtonName = "btn" + ID;
        else
            this.LinkButtonName = LinkButtonName;
        if (string.IsNullOrEmpty(LinkButtonTitle))
            this.LinkButtonTitle = ID.CamelSplit();
        else
            this.LinkButtonTitle = LinkButtonTitle;
        this.LinkButtonIcon = LinkButtonIcon;
        this.LinkButtonClass = LinkButtonClass;
        this.Order = Order;
        LinkType = linkType;
        ButtonType = buttonType;
    }
    public string ID { get; }
    public string Controller { get; }
    public string Action { get; }
    public string Area { get; }
    public int Order { get; }
    public string LinkButtonName { get; }
    public string LinkButtonIcon { get; }
    public string LinkButtonTitle { get; }
    public string LinkButtonClass { get; }
    public LinkType LinkType { get; }
    public ButtonType ButtonType { get; }
}
