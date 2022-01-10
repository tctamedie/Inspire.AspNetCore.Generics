namespace Inspire.Annotator.Annotations
{
    public class TableModel
    {
        public TableModel()
        {
            Columns = new List<ColumnModel>();
            Filters = new List<TableFilterModel>();
            NavigationLinks = new List<Link>();
            BreadCrumbs = new List<BreadCrumb>();
        }
        public string KeyField { get; set; }
        public string ForeignKey { get; set; }
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
    }
}
