using Inspire.Annotator.Annotations;
using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class BreadCrumbAttribute: Attribute
{
    /// <summary>
    /// Configures secondary navigation to particular controller
    /// </summary>
    /// <param name="order">relative position of the secondary navigation</param>
    /// <param name="controller">The MVC controller to which the link navigates to</param>
    /// <param name="area"> the MVC area for the controller</param>
    /// <param name="header">the title of the page</param>
    /// <param name="foreignKey">cforeignKey column in corresponding database entity in case the navigation is between database entities</param>
    /// <param name="action">the default action for the controller, if not supplied then the index action is default</param>
    public BreadCrumbAttribute(int order,string controller, string area, string header, string foreignKey="", string action="Index")
    {
        Order = order;
        Controller = controller;
        Action = action;
        Area = area;
        Header = header;
        ForeignKey = string.IsNullOrEmpty(foreignKey)? foreignKey: foreignKey.FirstLetterToLower();
    }
    public int Order { get;  }
    public string Controller { get;  }
    public string Action { get;  }
    public string Area { get;  }
    public string Header { get;  }
    public string ForeignKey { get; }

}
