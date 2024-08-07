﻿namespace Inspire.Annotations
{
    public class BreadCrumb
    {
        public BreadCrumb(int order, string controller, string area, string foreingKey, string action, string header, string toolTip)
        {
            Order = order;
            Controller = controller;
            Area = area;
            ForeignKey = foreingKey;
            Action = action;
            Header = header;
            ToolTip = toolTip;
        }
        public int Order { get; }
        public string Controller { get; }
        public string Action { get; }
        public string Area { get; }
        public string Header { get; set; }
        public string ToolTip { get; set; }
        public string PrependHeader { get; set; }
        public string RecordID { get; set; }
        public string ForeignKey { get; }

    }
}
