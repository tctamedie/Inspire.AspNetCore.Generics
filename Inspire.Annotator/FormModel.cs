namespace Inspire.Annotator.Annotations
{
    public class FormModel
    {
        public List<NavigationModel> NavigationModels { get; set; }
        public string ForegnKey { get; set; }
        public string ForegnKeyDesc { get; set; }
        public string KeyField { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
        public string Header { get; set; }
        public string Modal { get; set; }
        public List<TabModel> Tabs { get; set; }
    }
}
