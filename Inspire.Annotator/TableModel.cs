namespace Inspire.Annotations
{
    public class TableModel
    {
        public TableModel()
        {
            Columns = new List<ColumnModel>();
            Filters = new List<TableFilterModel>();
            NavigationLinks = new List<Link>();
            BreadCrumbs = new List<BreadCrumb>();
            Buttons = new List<ButtonModel>();
            ToggleButtons = new List<ToggleButtonModel>();
            SortButtons = new List<SortButtonModel>();
        }
        public string KeyField { get; set; }
        public string ForeignKey { get; set; }
        public string ForeignKeyDesc { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
        public string Header { get; set; }
        public string Modal { get; set; }
        public bool IsCreator { get; set; }
        public List<ColumnModel> Columns { get; set; }
        public List<TableFilterModel> Filters { get; set; }
        public List<Link> NavigationLinks { get; set; }

        public List<BreadCrumb> BreadCrumbs { get; set; }
        public List<ButtonModel> Buttons { get; set; }
        public List<ToggleButtonModel> ToggleButtons { get; set; }
        public List<SortButtonModel> SortButtons { get; set; }
    }
}
