using System.Runtime.CompilerServices;
public class Link 
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
    public Link(string Controller, string LinkButtonName = "", string LinkButtonTitle = "", string LinkButtonIcon = "fa fa-external-link", string LinkButtonClass = "btn btn-primary btn-xs", string Action = "Index", [CallerMemberName] string ID = "", string Area = "")
    {
        this.ID = ID;
        this.Controller = Controller;
        this.Action = Action;
        this.Area = Area;        
        this.LinkButtonName = LinkButtonName;
        this.LinkButtonTitle = LinkButtonTitle;
        this.LinkButtonIcon = LinkButtonIcon;
        this.LinkButtonClass = LinkButtonClass;
    }
    public string ID { get; }
    public string Controller { get; }
    public string Action { get; }
    public string Area { get; }
    public string LinkButtonName { get; }
    public string LinkButtonIcon { get; }
    public string LinkButtonTitle { get; }
    public string LinkButtonClass { get; }
}
