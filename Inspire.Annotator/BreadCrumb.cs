public class BreadCrumb
{
    public BreadCrumb(int order, string controller, string area, string foreingKey, string action, string header)
    {
        Order = order;
        Controller = controller;
        Area = area;
        ForeignKey = foreingKey;
        Action = action;
        Header = header;
    }
    public int Order { get; }
    public string Controller { get; }
    public string Action { get;  }
    public string Area { get; }
    public string Header { get; set; }
    public string PrependHeader { get; set; }
    public string RecordID { get; set; }
    public string ForeignKey { get;  }

}
